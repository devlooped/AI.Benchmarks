.HostEnvironmentInfo.ChronometerFrequency.Hertz as $ticks 
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
)