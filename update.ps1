git pull && `
pushd .\src\AI.Benchmarks && `
dotnet run -c Release 

if (-not $?) { throw 'Failed to run benchmarks' }

jq -r '.HostEnvironmentInfo.ChronometerFrequency.Hertz as $ticks 
| .Benchmarks 
| map(select((.Parameters 
  | (contains("-mini") or contains("-beta")) | not)))
| map({
  provider: .Parameters[7:10],
  model: .Parameters[11:],
  mean: ((.Statistics.Mean / $ticks) | . * 10 | floor | . / 1000)
}) 
| reduce .[] as $item ({};
  . + {($item.provider): {
    mean: $item.mean,
    model: $item.model
  }}
)' .\artifacts\results\AI.Benchmarks.ModelPerformance-report-full.json > .\artifacts\summary.json

if (-not $?) { throw 'Failed to create summary' }

.\resolve-file-includes.ps1
add *.json
add *.md
commit -m "🖉 Update AI benchmarks"
git push || write-host 'Failed to push updated benchmarks'