﻿// -----------------------------------------------------------------------
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
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly float slowDistance;
        private readonly float teleportDistance;
        private readonly Broadcast teleportMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="config">An instance of the <see cref="Config" /> class.</param>
        public EventHandlers(Config config)
        {
            slowDistance = config.SlowDistance * config.SlowDistance;
            teleportDistance = config.TeleportDistance * config.TeleportDistance;
            teleportMessage = config.TeleportMessage;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnWalkingOnSinkhole(WalkingOnSinkholeEventArgs)"/>
        public void OnWalkingOnSinkhole(WalkingOnSinkholeEventArgs ev)
        {
            ev.IsAllowed = false;
            if (ev.Player.IsScp && ev.Sinkhole.SCPImmune)
                return;

            float distance = (ev.Player.Position - ev.Sinkhole.transform.position).sqrMagnitude;
            if (distance <= teleportDistance)
            {
                ev.Player.Position = Vector3.down * -1998.5f;
                ev.Player.EnableEffect(EffectType.Corroding);
                ev.Player.DisableEffect(EffectType.SinkHole);
                ev.Player.Broadcast(teleportMessage);
                return;
            }

            if (distance <= slowDistance)
            {
                ev.Player.EnableEffect(EffectType.SinkHole);
                return;
            }

            ev.Player.DisableEffect(EffectType.SinkHole);
        }
    }
}