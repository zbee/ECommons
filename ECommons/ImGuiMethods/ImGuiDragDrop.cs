﻿using Dalamud.Bindings.ImGui;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ECommons.ImGuiMethods;
#nullable disable

// ImGui extra functionality related with Drag and Drop
public static class ImGuiDragDrop
{
    // TODO: review
    // can now pass refs with Unsafe.AsRef

    public static unsafe void SetDragDropPayload<T>(string type, T data, ImGuiCond cond = 0)
    where T : unmanaged
    {
        var ptr = Unsafe.AsPointer(ref data);
        ImGui.SetDragDropPayload(type, ptr, (uint)Unsafe.SizeOf<T>(), cond);
    }

    public static unsafe bool AcceptDragDropPayload<T>(string type, out T payload, ImGuiDragDropFlags flags = ImGuiDragDropFlags.None)
    where T : unmanaged
    {
        ImGuiPayload* pload = ImGui.AcceptDragDropPayload(type, flags);
        payload = (pload != null) ? Unsafe.Read<T>(pload->Data) : default;
        return pload != null;
    }

    public static unsafe void SetDragDropPayload(string type, string data, ImGuiCond cond = 0)
    {
        fixed(char* chars = data)
        {
            var byteCount = Encoding.Default.GetByteCount(data);
            var bytes = stackalloc byte[byteCount];
            Encoding.Default.GetBytes(chars, data.Length, bytes, byteCount);

            ImGui.SetDragDropPayload(type, bytes, (uint)byteCount, cond);
        }
    }

    public static unsafe bool AcceptDragDropPayload(string type, out string payload, ImGuiDragDropFlags flags = ImGuiDragDropFlags.None)
    {
        ImGuiPayload* pload = ImGui.AcceptDragDropPayload(type, flags);
        payload = (pload != null) ? Encoding.Default.GetString((byte*)pload->Data, pload->DataSize) : null;
        return pload != null;
    }
}
