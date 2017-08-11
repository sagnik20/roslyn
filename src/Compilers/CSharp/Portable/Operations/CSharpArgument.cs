﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis.Semantics;

namespace Microsoft.CodeAnalysis.CSharp
{
    internal abstract class BaseCSharpArgument : BaseArgument, IArgument
    {
        public BaseCSharpArgument(ArgumentKind argumentKind, IParameterSymbol parameter, SemanticModel semanticModel, SyntaxNode syntax, ITypeSymbol type, Optional<object> constantValue) :
            base(argumentKind, parameter, semanticModel, syntax, type, constantValue)
        {
        }

        public override CommonConversion InConversion => default(CommonConversion);

        public override CommonConversion OutConversion => default(CommonConversion);
    }

    internal sealed partial class CSharpArgument : BaseCSharpArgument
    {
        public CSharpArgument(ArgumentKind argumentKind, IParameterSymbol parameter, IOperation value, SemanticModel semanticModel, SyntaxNode syntax, ITypeSymbol type, Optional<object> constantValue) :
            base(argumentKind, parameter, semanticModel, syntax, type, constantValue)
        {
            ValueImpl = value;
        }

        protected override IOperation ValueImpl { get; }
    }

    internal sealed partial class LazyCSharpArgument : BaseCSharpArgument
    {
        private readonly Lazy<IOperation> _lazyValue;

        public LazyCSharpArgument(ArgumentKind argumentKind, IParameterSymbol parameter, Lazy<IOperation> value, SemanticModel semanticModel, SyntaxNode syntax, ITypeSymbol type, Optional<object> constantValue) :
            base(argumentKind, parameter, semanticModel, syntax, type, constantValue)
        {
            _lazyValue = value ?? throw new ArgumentNullException(nameof(value));
        }        

        protected override IOperation ValueImpl => _lazyValue.Value;
    }
}
