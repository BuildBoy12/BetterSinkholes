// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using Exiled.API.Enums;
    using Exiled.Events.EventArgs;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin" /> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnWalkingOnSinkhole(WalkingOnSinkholeEventArgs)"/>
        public void OnWalkingOnSinkhole(WalkingOnSinkholeEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsNPC"))
                return;

            if (ev.Player.IsScp && ev.Sinkhole.SCPImmune)
                return;

            if ((ev.Player.Position - ev.Sinkhole.transform.position).sqrMagnitude > plugin.Config.TeleportDistance * plugin.Config.TeleportDistance)
                return;

            ev.IsAllowed = false;
            ev.Player.DisableEffect(EffectType.SinkHole);

            ev.Player.ReferenceHub.scp106PlayerScript.GrabbedPosition = ev.Player.Position;
            ev.Player.EnableEffect(EffectType.Corroding);

            ev.Player.Hurt(plugin.Config.EntranceDamage, DamageType.PocketDimension);
            ev.Player.Broadcast(plugin.Config.TeleportMessage);
        }
    }
}