﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.CodeAnalysis.Interop
{
    internal static class ClrStrongName
    {
        [DllImport("mscoree.dll", PreserveSig = false, EntryPoint = "CLRCreateInstance")]
        [return: MarshalAs(UnmanagedType.Interface)]
        private static extern object nCreateInterface(
                [MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        internal static IClrStrongName GetInstance()
        {
            var metaHostClsid = new Guid(0x9280188D, 0xE8E, 0x4867, 0xB3, 0xC, 0x7F, 0xA8, 0x38, 0x84, 0xE8, 0xDE);
            var metaHostGuid = new Guid(0xD332DB9E, 0xB9B3, 0x4125, 0x82, 0x07, 0xA1, 0x48, 0x84, 0xF5, 0x32, 0x16);
            var clrStrongNameClsid = new Guid(0xB79B0ACD, 0xF5CD, 0x409b, 0xB5, 0xA5, 0xA1, 0x62, 0x44, 0x61, 0x0B, 0x92);
            var clrRuntimeInfoGuid = new Guid(0xBD39D1D2, 0xBA2F, 0x486A, 0x89, 0xB0, 0xB4, 0xB0, 0xCB, 0x46, 0x68, 0x91);
            var clrStrongNameGuid = new Guid(0x9FD93CCF, 0x3280, 0x4391, 0xB3, 0xA9, 0x96, 0xE1, 0xCD, 0xE7, 0x7C, 0x8D);

            var metaHost = (IClrMetaHost)nCreateInterface(metaHostClsid, metaHostGuid);
            var runtime = (IClrRuntimeInfo)metaHost.GetRuntime(RuntimeEnvironment.GetSystemVersion(), clrRuntimeInfoGuid);
            return (IClrStrongName)runtime.GetInterface(clrStrongNameClsid, clrStrongNameGuid);
        }
    }
}
