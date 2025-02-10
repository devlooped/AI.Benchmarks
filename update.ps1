git pull && `
pushd .\src\AI.Benchmarks && `
dotnet run -c Release && `
popd && `
add *.json && `
add *.md && `
commit -m "🖉 Update AI benchmarks" && `
git push || write-host 'Failed to update benchmarks'