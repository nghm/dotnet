Something
    .Like((dependency) => new AsyncStepBuilder(dependency))
    .Should(() => throw new ArgumentNullException());