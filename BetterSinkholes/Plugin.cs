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
        public override Version RequiredExiledVersion { get; } = new(5, 3, 0);

        /// <inheritdoc />
        public override Version Version { get; } = new(4, 2, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard += eventHandlers.OnEnteringEnvironmentalHazard;
            Exiled.Events.Handlers.Player.StayingOnEnvironmentalHazard += eventHandlers.OnStayingOnEnvironmentalHazard;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard -= eventHandlers.OnEnteringEnvironmentalHazard;
            Exiled.Events.Handlers.Player.StayingOnEnvironmentalHazard -= eventHandlers.OnStayingOnEnvironmentalHazard;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}
