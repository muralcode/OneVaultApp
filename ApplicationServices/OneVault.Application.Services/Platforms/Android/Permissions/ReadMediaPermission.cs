using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVault.ApplicationServices.Platforms.Android.Permissions
{
    internal sealed class ReadMediaPermission: Microsoft.Maui.ApplicationModel.Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
            new List<(string androidPermission, bool isRuntime)>
            {
                (global::Android.Manifest.Permission.ReadMediaVideo, true),
                (global::Android.Manifest.Permission.ReadMediaImages, true)
            }.ToArray();
    }
}
