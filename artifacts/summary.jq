.HostEnvironmentInfo.ChronometerFrequency.Hertz as $ticks 
| .Benchmarks 
| map({
  provider: .Parameters[7:10],
  model: .Parameters[11:],
  mean: ((.Statistics.Mean / $ticks) | . * 10 | floor | . / 1000)
}) 
| reduce .[] as $item ({};
  . + {($item.provider): (
    .[$item.provider] // {} | . + {($item.model): $item.mean}
  )}
)