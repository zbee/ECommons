﻿using ECommons.DalamudServices;
using System;
using System.Collections.Generic;

namespace ECommons.EzEventManager;

/// <summary>
/// Provides wrapped access to ClientState.Logout event. Disposed automatically upon calling <see cref="ECommonsMain.Dispose"/>.
/// </summary>
public class EzLogout : IDisposable
{
    internal static List<EzLogout> Registered = [];
    internal Action Delegate;

    public EzLogout(Action @delegate)
    {
        Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        Svc.ClientState.Logout += ClientState_Logout;
        Registered.Add(this);
    }

    private void ClientState_Logout(int type, int code)
    {
        Delegate();
    }

    public void Dispose()
    {
        Svc.ClientState.Logout -= ClientState_Logout;
        Registered.Remove(this);
    }
}
