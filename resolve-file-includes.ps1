$include = $env:RESOLVE_INCLUDE ?? '*.md'
$exclude = $env:RESOLVE_EXCLUDE ?? $null
$recurse = [bool]::Parse($env:RESOLVE_RECURSE ?? "true")
$validate = [bool]::Parse($env:RESOLVE_VALIDATE ?? "true")

Write-Output "Include: $include"
Write-Output "Exclude: $exclude"

$files = if ($recurse) {
  Get-ChildItem -Include $include -Exclude $exclude -Recurse
} else {
  Get-ChildItem -Include $include -Exclude $exclude
}

foreach ($file in $files) {
    Write-Output "Processing $file"
    $content = ((Get-Content $file -Raw -Encoding UTF8) ?? '').Trim()
    if ($content.StartsWith('<!-- exclude -->') -or $content.EndsWith('<!-- exclude -->')) {
        Write-Output "Excluding $file"
        continue
    }

    $replacements = @{}
    foreach ($match in (Select-String -Pattern '<!--\s?include (.*?)\s?-->' -Path $file)) {
        $includedPath = ($match.Matches[0].Value -replace '<!--\s?include ','' -replace '\s?-->', '').Trim()
        $fragment = $null
        if ($includedPath.Contains('#')) {
            $fragment = '#' + $includedPath.Split('#')[1]
            $includedPath = $includedPath.Substring(0, $includedPath.IndexOf('#'))
        }

        $isuri = [uri]::IsWellFormedUriString($includedPath, 'Absolute')
        $includedFullPath = Join-Path (Get-ChildItem -Path $file).DirectoryName -ChildPath $includedPath

        if ((Test-Path $includedFullPath) -Or $isuri) {
            $include = if ($isuri) {
                (Invoke-RestMethod $includedPath) ?? ''
            } else {
                (Get-Content $includedFullPath -Raw -Encoding UTF8) ?? ''
            }

            # Resolve fragment specifier if present
            if ($fragment) {
                $anchor = "<!-- $fragment -->"
                $start = $include.IndexOf($anchor)
                if ($start -eq -1) {
                    if ($validate) {
                        Write-Error "Referenced anchor $fragment not found in $includedPath"
                    } else {
                        Write-Warning "Referenced anchor $fragment not found in $includedPath"
                    }
                    continue
                }
                $include = $include.Substring($start)
                $end = $include.IndexOf($anchor, $anchor.Length)
                if ($end -ne -1) {
                    $include = $include.Substring(0, $end + $anchor.Length)
                }
            }
            # TODO: disable nested includes until we can properly support that scenario, and nested excludes
            $include = $include -replace '<!--\s?include ','<!-- ' -replace '<!-- exclude -->', ''
            # see if we already have a section we previously replaced
            $existingRegex = "<!--\s?include $includedPath$fragment\s?-->[\s\S]*<!-- $includedPath$fragment -->"
            $replacement = "<!-- include $includedPath$fragment -->`n$include`n<!-- $includedPath$fragment -->"
            if ($content -match $existingRegex) {
                $replacements[$existingRegex] = $replacement
            } else {
                $replacements["<!--\s?include $includedPath$fragment\s?-->"] = $replacement
            }
        } else {
            if ($validate) {
                Write-Error "Included file $includedPath in $($file.Name) not found" 
            } else {
                Write-Warning "Included file $includedPath in $($file.Name) not found" 
            }
        }
    }

    if ($replacements.Count -gt 0) {
        foreach ($replacement in $replacements.GetEnumerator()) {
            #Write-Host "Replacing $($replacement.Key) with $($replacement.Value)"
            $content = $content -replace $replacement.Key, $replacement.Value
        }

        $content = $content.TrimEnd()
        $actual = (Get-Content $file -Raw -Encoding UTF8).TrimEnd()
        
        if ($content -ne $actual) {
            Set-Content $file -Value $content.TrimEnd() -Encoding UTF8
            Write-Output "Updated $($file.Name)"
        }
    }
}