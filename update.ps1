git pull && `
pushd .\src\AI.Benchmarks && `
dotnet run -c Release --no-launch-profile

if (-not $?) { throw 'Failed to run benchmarks' }

popd;
pushd .\artifacts

jq -r -f summary.jq .\results\AI.Benchmarks.ModelPerformance-report-full.json > summary.json

if (-not $?) { throw 'Failed to create summary' }

popd;
.\resolve-file-includes.ps1
git add *.json
git add *.md
git commit -m "🖉 Update AI benchmarks"
git push || write-host 'Failed to push updated benchmarks'