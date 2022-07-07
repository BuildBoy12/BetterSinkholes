// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
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

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnEnteringEnvironmentalHazard(EnteringEnvironmentalHazardEventArgs)"/>
        public void OnEnteringEnvironmentalHazard(EnteringEnvironmentalHazardEventArgs ev)
        {
            if (ShouldAffect(ev.EnvironmentalHazard, ev.Player))
                ev.IsAllowed = false;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnStayingOnEnvironmentalHazard(StayingOnEnvironmentalHazardEventArgs)"/>
        public void OnStayingOnEnvironmentalHazard(StayingOnEnvironmentalHazardEventArgs ev)
        {
            if (ShouldAffect(ev.EnvironmentalHazard, ev.Player))
                ev.IsAllowed = false;
        }

        private bool ShouldAffect(EnvironmentalHazard environmentalHazard, Player player)
        {
            if (environmentalHazard is not SinkholeEnvironmentalHazard || player.IsScp || player.SessionVariables.ContainsKey("IsNPC"))
                return false;

            if ((player.Position - environmentalHazard.transform.position).sqrMagnitude > plugin.Config.TeleportDistance * plugin.Config.TeleportDistance)
                return false;

            player.DisableEffect(EffectType.SinkHole);

            player.ReferenceHub.scp106PlayerScript.GrabbedPosition = player.Position;
            player.EnableEffect(EffectType.Corroding);

            player.Hurt(plugin.Config.EntranceDamage, DamageType.PocketDimension);
            player.Broadcast(plugin.Config.TeleportMessage);
            return true;
        }
    }
}