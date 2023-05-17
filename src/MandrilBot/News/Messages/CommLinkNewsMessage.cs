﻿using System.Diagnostics.CodeAnalysis;

namespace MandrilBot.News.Messages
{
    public struct CommLinkNewsMessage
    {
        /// <summary>
        /// Title of the post.
        /// </summary>
        public string Title;
        /// <summary>
        /// Description of the post.
        /// </summary>
        public string Description;
        /// <summary>
        /// Official source link of the post.
        /// </summary>
        public string SourceLink;
        /// <summary>
        /// Official image link of the post.
        /// </summary>
        public string ImageLink;
    }

    /// <summary>
    /// Custom Equality comparer for CommLinkNewsMessage needed to ignore Date as it is changing every hour or minute, it depends see <see cref="CommLinkNewsMessage.Date"/>
    /// </summary>
    public class CommLinkNewsMessageComparer : IEqualityComparer<CommLinkNewsMessage>
    {
        public bool Equals(CommLinkNewsMessage x, CommLinkNewsMessage y)
            => x.Title == y.Title
               && x.SourceLink == y.SourceLink;

        public int GetHashCode([DisallowNull] CommLinkNewsMessage lObj)
            => HashCode.Combine(lObj.Title, lObj.SourceLink);

    }
}
