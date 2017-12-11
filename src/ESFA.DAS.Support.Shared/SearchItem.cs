﻿using System.Diagnostics.CodeAnalysis;

namespace ESFA.DAS.Support.Shared
{
    [ExcludeFromCodeCoverage]
    public class SearchItem
    {
        public string SearchId { get; set; }

        public string[] Keywords { get; set; }

        public string Html { get; set; }
    }
}