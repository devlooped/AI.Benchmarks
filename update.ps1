﻿git pull && `
pushd .\src\AI.Benchmarks && `
dotnet run -c Release 

if (-not $?) { throw 'Failed to run benchmarks' }

jq -r '.HostEnvironmentInfo.ChronometerFrequency.Hertz as $ticks | 
.Benchmarks[] | 
{
    Provider: (({
        aai: "Azure AI",
        oai: "OpenAI",
        xai: "xAI" 
    })[.Parameters[7:10]] // "Unknown"),
    Model: .Parameters[11:],
    # express as seconds to match markdown
    Mean: ((.Statistics.Mean / $ticks) | . * 10 | floor | . / 1000)
}' .\BenchmarkDotNet.Artifacts\results\AI.Benchmarks.ModelPerformance-report-full.json > .\BenchmarkDotNet.Artifacts\results\summary.json

if (-not $?) { throw 'Failed to create summary' }

add *.json
add *.md
commit -m "🖉 Update AI benchmarks"
git push || write-host 'Failed to push updated benchmarks'