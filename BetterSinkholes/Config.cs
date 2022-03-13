// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the distance from the center of a sinkhole where a player gets teleported.
        /// </summary>
        [Description("The distance from the center of a sinkhole where a player gets teleported. This is limited to inside the sinkhole's range.")]
        public float TeleportDistance { get; set; } = 2f;

        /// <summary>
        /// Gets or sets the amount of damage to deal to someone when they fall into the pocket dimension.
        /// </summary>
        [Description("Thea amount of damage to deal to someone when they fall into the pocket dimension")]
        public float EntranceDamage { get; set; } = 40;

        /// <summary>
        /// Gets or sets the message to show to someone when they fall into the pocket dimension.
        /// </summary>
        [Description("The message to show to someone when they fall into the pocket dimension.")]
        public Broadcast TeleportMessage { get; set; } = new Broadcast
        {
            Content = "You've fallen into the pocket dimension!",
            Duration = 5,
            Show = false,
        };
    }
}
