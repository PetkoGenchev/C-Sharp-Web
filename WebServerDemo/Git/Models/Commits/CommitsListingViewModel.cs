﻿using System;

namespace Git.Models.Commits
{
    public class CommitsListingViewModel
    {
        public string Id { get; init; }
        public string RepositoryName { get; init; }
        public string Description { get; init; }
        public DateTime CreatedOn { get; init; }

    }
}
