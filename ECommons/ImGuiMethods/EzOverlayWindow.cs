﻿using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ECommons.Logging;
using Dalamud.Bindings.ImGui;
using System.Numerics;

namespace ECommons.ImGuiMethods;
public abstract class EzOverlayWindow : Window
{
    public HorizontalPosition HPos;
    public VerticalPosition VPos;
    public Vector2 Offset;
    public Vector2 WindowSize;

    public EzOverlayWindow(string name, HorizontalPosition hPos, VerticalPosition vPos, Vector2? offset = null) : base(name, ImGuiEx.OverlayFlags | ImGuiWindowFlags.AlwaysAutoResize, true)
    {
        RespectCloseHotkey = false;
        IsOpen = true;
        VPos = vPos;
        HPos = hPos;
        Offset = offset ?? Vector2.Zero;
    }

    public virtual void PreDrawAction() { }

    public sealed override void PreDraw()
    {
        PreDrawAction();
        var vportsize = ImGuiHelpers.MainViewport.Size;
        var x = 0f;
        var y = 0f;
        if(HPos == HorizontalPosition.Middle) x = vportsize.X / 2 - WindowSize.X / 2;
        if(HPos == HorizontalPosition.Right) x = vportsize.X - WindowSize.X;
        if(VPos == VerticalPosition.Middle) y = vportsize.Y / 2 - WindowSize.Y / 2;
        if(VPos == VerticalPosition.Bottom) y = vportsize.Y - WindowSize.Y;
        var vec = new Vector2(x, y) + Offset;
        //PluginLog.Information($"Set offset to: {vec}");
        ImGuiHelpers.SetNextWindowPosRelativeMainViewport(vec);
    }

    public abstract void DrawAction();

    public sealed override void Draw()
    {
        DrawAction();
        WindowSize = ImGui.GetWindowSize();
    }

    public enum VerticalPosition { Top, Middle, Bottom }
    public enum HorizontalPosition { Left, Middle, Right }
}
