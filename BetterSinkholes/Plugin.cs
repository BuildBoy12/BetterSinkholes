// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using System;
    using Exiled.API.Features;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        /// <inheritdoc />
        public override string Author => "Build";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new(5, 2, 2);

        /// <inheritdoc />
        public override Version Version { get; } = new(4, 1, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.WalkingOnSinkhole += eventHandlers.OnWalkingOnSinkhole;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.WalkingOnSinkhole -= eventHandlers.OnWalkingOnSinkhole;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}
