; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [311 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [616 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 69
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 68
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 109
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 231
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 265
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 49
	i32 38948123, ; 6: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 273
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 81
	i32 42244203, ; 8: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 282
	i32 42639949, ; 9: System.Threading.Thread => 0x28aa24d => 146
	i32 66541672, ; 10: System.Diagnostics.StackTrace => 0x3f75868 => 31
	i32 67008169, ; 11: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 306
	i32 68219467, ; 12: System.Security.Cryptography.Primitives => 0x410f24b => 125
	i32 72070932, ; 13: Microsoft.Maui.Graphics.dll => 0x44bb714 => 189
	i32 82292897, ; 14: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 103
	i32 83839681, ; 15: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 290
	i32 101534019, ; 16: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 249
	i32 117431740, ; 17: System.Runtime.InteropServices => 0x6ffddbc => 108
	i32 120558881, ; 18: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 249
	i32 122350210, ; 19: System.Threading.Channels.dll => 0x74aea82 => 140
	i32 134690465, ; 20: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 269
	i32 136584136, ; 21: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 305
	i32 140062828, ; 22: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 298
	i32 142721839, ; 23: System.Net.WebHeaderCollection => 0x881c32f => 78
	i32 149972175, ; 24: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 125
	i32 159306688, ; 25: System.ComponentModel.Annotations => 0x97ed3c0 => 14
	i32 165246403, ; 26: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 205
	i32 176265551, ; 27: System.ServiceProcess => 0xa81994f => 133
	i32 182336117, ; 28: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 251
	i32 184328833, ; 29: System.ValueTuple.dll => 0xafca281 => 152
	i32 205061960, ; 30: System.ComponentModel => 0xc38ff48 => 19
	i32 209399409, ; 31: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 203
	i32 220171995, ; 32: System.Diagnostics.Debug => 0xd1f8edb => 27
	i32 221514239, ; 33: AIpaca_App.resources.dll => 0xd3409ff => 0
	i32 230216969, ; 34: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 225
	i32 230752869, ; 35: Microsoft.CSharp.dll => 0xdc10265 => 2
	i32 231409092, ; 36: System.Linq.Parallel => 0xdcb05c4 => 60
	i32 231814094, ; 37: System.Globalization => 0xdd133ce => 43
	i32 246610117, ; 38: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 92
	i32 261689757, ; 39: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 208
	i32 276479776, ; 40: System.Threading.Timer.dll => 0x107abf20 => 148
	i32 278686392, ; 41: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 227
	i32 280482487, ; 42: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 224
	i32 291076382, ; 43: System.IO.Pipes.AccessControl.dll => 0x1159791e => 55
	i32 298918909, ; 44: System.Net.Ping.dll => 0x11d123fd => 70
	i32 317674968, ; 45: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 303
	i32 318968648, ; 46: Xamarin.AndroidX.Activity.dll => 0x13031348 => 194
	i32 321597661, ; 47: System.Numerics => 0x132b30dd => 84
	i32 321963286, ; 48: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 281
	i32 342366114, ; 49: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 226
	i32 360082299, ; 50: System.ServiceModel.Web => 0x15766b7b => 132
	i32 367780167, ; 51: System.IO.Pipes => 0x15ebe147 => 56
	i32 374914964, ; 52: System.Transactions.Local => 0x1658bf94 => 150
	i32 375677976, ; 53: System.Net.ServicePoint.dll => 0x16646418 => 75
	i32 379916513, ; 54: System.Threading.Thread.dll => 0x16a510e1 => 146
	i32 385762202, ; 55: System.Memory.dll => 0x16fe439a => 63
	i32 392610295, ; 56: System.Threading.ThreadPool.dll => 0x1766c1f7 => 147
	i32 395744057, ; 57: _Microsoft.Android.Resource.Designer => 0x17969339 => 307
	i32 403441872, ; 58: WindowsBase => 0x180c08d0 => 166
	i32 409257351, ; 59: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 301
	i32 441335492, ; 60: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 209
	i32 442565967, ; 61: System.Collections => 0x1a61054f => 13
	i32 450948140, ; 62: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 222
	i32 451504562, ; 63: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 126
	i32 456227837, ; 64: System.Web.HttpUtility.dll => 0x1b317bfd => 153
	i32 459347974, ; 65: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 114
	i32 465846621, ; 66: mscorlib => 0x1bc4415d => 167
	i32 469710990, ; 67: System.dll => 0x1bff388e => 165
	i32 476646585, ; 68: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 224
	i32 486930444, ; 69: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 237
	i32 489220957, ; 70: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 279
	i32 498788369, ; 71: System.ObjectModel => 0x1dbae811 => 85
	i32 513247710, ; 72: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 183
	i32 526420162, ; 73: System.Transactions.dll => 0x1f6088c2 => 151
	i32 527452488, ; 74: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 269
	i32 530272170, ; 75: System.Linq.Queryable => 0x1f9b4faa => 61
	i32 538707440, ; 76: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 300
	i32 539058512, ; 77: Microsoft.Extensions.Logging => 0x20216150 => 179
	i32 540030774, ; 78: System.IO.FileSystem.dll => 0x20303736 => 52
	i32 545304856, ; 79: System.Runtime.Extensions => 0x2080b118 => 104
	i32 546455878, ; 80: System.Runtime.Serialization.Xml => 0x20924146 => 115
	i32 549171840, ; 81: System.Globalization.Calendars => 0x20bbb280 => 41
	i32 557405415, ; 82: Jsr305Binding => 0x213954e7 => 262
	i32 569601784, ; 83: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 260
	i32 577335427, ; 84: System.Security.Cryptography.Cng => 0x22697083 => 121
	i32 601371474, ; 85: System.IO.IsolatedStorage.dll => 0x23d83352 => 53
	i32 605376203, ; 86: System.IO.Compression.FileSystem => 0x24154ecb => 45
	i32 613668793, ; 87: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 120
	i32 627609679, ; 88: Xamarin.AndroidX.CustomView => 0x2568904f => 214
	i32 627931235, ; 89: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 292
	i32 639843206, ; 90: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 220
	i32 643868501, ; 91: System.Net => 0x2660a755 => 82
	i32 662205335, ; 92: System.Text.Encodings.Web.dll => 0x27787397 => 137
	i32 663517072, ; 93: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 256
	i32 666292255, ; 94: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 201
	i32 672442732, ; 95: System.Collections.Concurrent => 0x2814a96c => 9
	i32 674948931, ; 96: AIpaca_App.resources => 0x283ae743 => 0
	i32 683518922, ; 97: System.Net.Security => 0x28bdabca => 74
	i32 690569205, ; 98: System.Xml.Linq.dll => 0x29293ff5 => 156
	i32 691348768, ; 99: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 271
	i32 693804605, ; 100: System.Windows => 0x295a9e3d => 155
	i32 699345723, ; 101: System.Reflection.Emit => 0x29af2b3b => 93
	i32 700284507, ; 102: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 266
	i32 700358131, ; 103: System.IO.Compression.ZipFile => 0x29be9df3 => 46
	i32 720511267, ; 104: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 270
	i32 722857257, ; 105: System.Runtime.Loader.dll => 0x2b15ed29 => 110
	i32 735137430, ; 106: System.Security.SecureString.dll => 0x2bd14e96 => 130
	i32 752232764, ; 107: System.Diagnostics.Contracts.dll => 0x2cd6293c => 26
	i32 755313932, ; 108: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 191
	i32 759454413, ; 109: System.Net.Requests => 0x2d445acd => 73
	i32 762598435, ; 110: System.IO.Pipes.dll => 0x2d745423 => 56
	i32 775507847, ; 111: System.IO.Compression => 0x2e394f87 => 47
	i32 777317022, ; 112: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 298
	i32 789151979, ; 113: Microsoft.Extensions.Options => 0x2f0980eb => 182
	i32 790371945, ; 114: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 215
	i32 804715423, ; 115: System.Data.Common => 0x2ff6fb9f => 23
	i32 807930345, ; 116: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 229
	i32 823281589, ; 117: System.Private.Uri.dll => 0x311247b5 => 87
	i32 830298997, ; 118: System.IO.Compression.Brotli => 0x317d5b75 => 44
	i32 832635846, ; 119: System.Xml.XPath.dll => 0x31a103c6 => 161
	i32 834051424, ; 120: System.Net.Quic => 0x31b69d60 => 72
	i32 843511501, ; 121: Xamarin.AndroidX.Print => 0x3246f6cd => 242
	i32 869139383, ; 122: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 283
	i32 873119928, ; 123: Microsoft.VisualBasic => 0x340ac0b8 => 4
	i32 877678880, ; 124: System.Globalization.dll => 0x34505120 => 43
	i32 878954865, ; 125: System.Net.Http.Json => 0x3463c971 => 64
	i32 880668424, ; 126: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 297
	i32 904024072, ; 127: System.ComponentModel.Primitives.dll => 0x35e25008 => 17
	i32 911108515, ; 128: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 54
	i32 918734561, ; 129: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 294
	i32 928116545, ; 130: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 265
	i32 952186615, ; 131: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 106
	i32 956575887, ; 132: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 270
	i32 961460050, ; 133: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 287
	i32 966729478, ; 134: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 263
	i32 967690846, ; 135: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 226
	i32 975236339, ; 136: System.Diagnostics.Tracing => 0x3a20ecf3 => 35
	i32 975874589, ; 137: System.Xml.XDocument => 0x3a2aaa1d => 159
	i32 986514023, ; 138: System.Private.DataContractSerialization.dll => 0x3acd0267 => 86
	i32 987214855, ; 139: System.Diagnostics.Tools => 0x3ad7b407 => 33
	i32 990727110, ; 140: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 296
	i32 992768348, ; 141: System.Collections.dll => 0x3b2c715c => 13
	i32 994442037, ; 142: System.IO.FileSystem => 0x3b45fb35 => 52
	i32 1001831731, ; 143: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 57
	i32 1012816738, ; 144: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 246
	i32 1019214401, ; 145: System.Drawing => 0x3cbffa41 => 37
	i32 1028951442, ; 146: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 178
	i32 1031528504, ; 147: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 264
	i32 1035644815, ; 148: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 199
	i32 1036536393, ; 149: System.Drawing.Primitives.dll => 0x3dc84a49 => 36
	i32 1043950537, ; 150: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 277
	i32 1044663988, ; 151: System.Linq.Expressions.dll => 0x3e444eb4 => 59
	i32 1052210849, ; 152: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 233
	i32 1067306892, ; 153: GoogleGson => 0x3f9dcf8c => 174
	i32 1082857460, ; 154: System.ComponentModel.TypeConverter => 0x408b17f4 => 18
	i32 1084122840, ; 155: Xamarin.Kotlin.StdLib => 0x409e66d8 => 267
	i32 1098259244, ; 156: System => 0x41761b2c => 165
	i32 1108272742, ; 157: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 299
	i32 1117529484, ; 158: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 293
	i32 1118262833, ; 159: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 289
	i32 1121599056, ; 160: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 232
	i32 1127624469, ; 161: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 181
	i32 1149092582, ; 162: Xamarin.AndroidX.Window => 0x447dc2e6 => 259
	i32 1168523401, ; 163: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 295
	i32 1170634674, ; 164: System.Web.dll => 0x45c677b2 => 154
	i32 1175144683, ; 165: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 255
	i32 1178241025, ; 166: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 240
	i32 1204270330, ; 167: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 201
	i32 1208641965, ; 168: System.Diagnostics.Process => 0x480a69ad => 30
	i32 1219128291, ; 169: System.IO.IsolatedStorage => 0x48aa6be3 => 53
	i32 1243150071, ; 170: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 260
	i32 1253011324, ; 171: Microsoft.Win32.Registry => 0x4aaf6f7c => 6
	i32 1260983243, ; 172: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 275
	i32 1264511973, ; 173: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 250
	i32 1267360935, ; 174: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 254
	i32 1273260888, ; 175: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 206
	i32 1275534314, ; 176: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 271
	i32 1278448581, ; 177: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 198
	i32 1293217323, ; 178: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 217
	i32 1308624726, ; 179: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 284
	i32 1309188875, ; 180: System.Private.DataContractSerialization => 0x4e08a30b => 86
	i32 1322716291, ; 181: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 259
	i32 1324164729, ; 182: System.Linq => 0x4eed2679 => 62
	i32 1335329327, ; 183: System.Runtime.Serialization.Json.dll => 0x4f97822f => 113
	i32 1336711579, ; 184: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 304
	i32 1364015309, ; 185: System.IO => 0x514d38cd => 58
	i32 1373134921, ; 186: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 305
	i32 1376866003, ; 187: Xamarin.AndroidX.SavedState => 0x52114ed3 => 246
	i32 1379779777, ; 188: System.Resources.ResourceManager => 0x523dc4c1 => 100
	i32 1402170036, ; 189: System.Configuration.dll => 0x53936ab4 => 20
	i32 1406073936, ; 190: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 210
	i32 1408764838, ; 191: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 112
	i32 1411638395, ; 192: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 102
	i32 1422545099, ; 193: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 103
	i32 1430672901, ; 194: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 273
	i32 1434145427, ; 195: System.Runtime.Handles => 0x557b5293 => 105
	i32 1435222561, ; 196: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 263
	i32 1439761251, ; 197: System.Net.Quic.dll => 0x55d10363 => 72
	i32 1452070440, ; 198: System.Formats.Asn1.dll => 0x568cd628 => 39
	i32 1453312822, ; 199: System.Diagnostics.Tools.dll => 0x569fcb36 => 33
	i32 1457743152, ; 200: System.Runtime.Extensions.dll => 0x56e36530 => 104
	i32 1458022317, ; 201: System.Net.Security.dll => 0x56e7a7ad => 74
	i32 1461004990, ; 202: es\Microsoft.Maui.Controls.resources => 0x57152abe => 279
	i32 1461234159, ; 203: System.Collections.Immutable.dll => 0x5718a9ef => 10
	i32 1461719063, ; 204: System.Security.Cryptography.OpenSsl => 0x57201017 => 124
	i32 1462112819, ; 205: System.IO.Compression.dll => 0x57261233 => 47
	i32 1469204771, ; 206: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 200
	i32 1470490898, ; 207: Microsoft.Extensions.Primitives => 0x57a5e912 => 183
	i32 1479771757, ; 208: System.Collections.Immutable => 0x5833866d => 10
	i32 1480492111, ; 209: System.IO.Compression.Brotli.dll => 0x583e844f => 44
	i32 1487239319, ; 210: Microsoft.Win32.Primitives => 0x58a57897 => 5
	i32 1490025113, ; 211: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 247
	i32 1526286932, ; 212: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 303
	i32 1536373174, ; 213: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 32
	i32 1543031311, ; 214: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 139
	i32 1543355203, ; 215: System.Reflection.Emit.dll => 0x5bfdbb43 => 93
	i32 1550322496, ; 216: System.Reflection.Extensions.dll => 0x5c680b40 => 94
	i32 1565862583, ; 217: System.IO.FileSystem.Primitives => 0x5d552ab7 => 50
	i32 1566207040, ; 218: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 142
	i32 1573704789, ; 219: System.Runtime.Serialization.Json => 0x5dccd455 => 113
	i32 1580037396, ; 220: System.Threading.Overlapped => 0x5e2d7514 => 141
	i32 1582372066, ; 221: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 216
	i32 1592978981, ; 222: System.Runtime.Serialization.dll => 0x5ef2ee25 => 116
	i32 1597949149, ; 223: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 264
	i32 1601112923, ; 224: System.Xml.Serialization => 0x5f6f0b5b => 158
	i32 1604827217, ; 225: System.Net.WebClient => 0x5fa7b851 => 77
	i32 1618516317, ; 226: System.Net.WebSockets.Client.dll => 0x6078995d => 80
	i32 1622152042, ; 227: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 236
	i32 1622358360, ; 228: System.Dynamic.Runtime => 0x60b33958 => 38
	i32 1624863272, ; 229: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 258
	i32 1635184631, ; 230: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 220
	i32 1636350590, ; 231: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 213
	i32 1639515021, ; 232: System.Net.Http.dll => 0x61b9038d => 65
	i32 1639986890, ; 233: System.Text.RegularExpressions => 0x61c036ca => 139
	i32 1641389582, ; 234: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 16
	i32 1657153582, ; 235: System.Runtime => 0x62c6282e => 117
	i32 1658241508, ; 236: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 252
	i32 1658251792, ; 237: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 261
	i32 1670060433, ; 238: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 208
	i32 1675553242, ; 239: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 49
	i32 1677501392, ; 240: System.Net.Primitives.dll => 0x63fca3d0 => 71
	i32 1678508291, ; 241: System.Net.WebSockets => 0x640c0103 => 81
	i32 1679769178, ; 242: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1691477237, ; 243: System.Reflection.Metadata => 0x64d1e4f5 => 95
	i32 1696967625, ; 244: System.Security.Cryptography.Csp => 0x6525abc9 => 122
	i32 1698840827, ; 245: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 268
	i32 1701541528, ; 246: System.Diagnostics.Debug.dll => 0x656b7698 => 27
	i32 1720223769, ; 247: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 229
	i32 1726116996, ; 248: System.Reflection.dll => 0x66e27484 => 98
	i32 1728033016, ; 249: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 29
	i32 1729485958, ; 250: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 204
	i32 1743415430, ; 251: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 274
	i32 1744735666, ; 252: System.Transactions.Local.dll => 0x67fe8db2 => 150
	i32 1746316138, ; 253: Mono.Android.Export => 0x6816ab6a => 170
	i32 1750313021, ; 254: Microsoft.Win32.Primitives.dll => 0x6853a83d => 5
	i32 1758240030, ; 255: System.Resources.Reader.dll => 0x68cc9d1e => 99
	i32 1763938596, ; 256: System.Diagnostics.TraceSource.dll => 0x69239124 => 34
	i32 1765942094, ; 257: System.Reflection.Extensions => 0x6942234e => 94
	i32 1766324549, ; 258: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 251
	i32 1770582343, ; 259: Microsoft.Extensions.Logging.dll => 0x6988f147 => 179
	i32 1776026572, ; 260: System.Core.dll => 0x69dc03cc => 22
	i32 1777075843, ; 261: System.Globalization.Extensions.dll => 0x69ec0683 => 42
	i32 1780572499, ; 262: Mono.Android.Runtime.dll => 0x6a216153 => 171
	i32 1782862114, ; 263: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 290
	i32 1788241197, ; 264: Xamarin.AndroidX.Fragment => 0x6a96652d => 222
	i32 1789713428, ; 265: AIpaca_App => 0x6aacdc14 => 1
	i32 1793755602, ; 266: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 282
	i32 1808609942, ; 267: Xamarin.AndroidX.Loader => 0x6bcd3296 => 236
	i32 1813058853, ; 268: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 267
	i32 1813201214, ; 269: Xamarin.Google.Android.Material => 0x6c13413e => 261
	i32 1818569960, ; 270: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 241
	i32 1818787751, ; 271: Microsoft.VisualBasic.Core => 0x6c687fa7 => 3
	i32 1824175904, ; 272: System.Text.Encoding.Extensions => 0x6cbab720 => 135
	i32 1824722060, ; 273: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 112
	i32 1828688058, ; 274: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 180
	i32 1847515442, ; 275: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 191
	i32 1853025655, ; 276: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 299
	i32 1858542181, ; 277: System.Linq.Expressions => 0x6ec71a65 => 59
	i32 1870277092, ; 278: System.Reflection.Primitives => 0x6f7a29e4 => 96
	i32 1875935024, ; 279: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 281
	i32 1879696579, ; 280: System.Formats.Tar.dll => 0x7009e4c3 => 40
	i32 1885316902, ; 281: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 202
	i32 1888955245, ; 282: System.Diagnostics.Contracts => 0x70972b6d => 26
	i32 1889954781, ; 283: System.Reflection.Metadata.dll => 0x70a66bdd => 95
	i32 1893218855, ; 284: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 275
	i32 1898237753, ; 285: System.Reflection.DispatchProxy => 0x7124cf39 => 90
	i32 1900610850, ; 286: System.Resources.ResourceManager.dll => 0x71490522 => 100
	i32 1910275211, ; 287: System.Collections.NonGeneric.dll => 0x71dc7c8b => 11
	i32 1939592360, ; 288: System.Private.Xml.Linq => 0x739bd4a8 => 88
	i32 1953182387, ; 289: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 286
	i32 1956758971, ; 290: System.Resources.Writer => 0x74a1c5bb => 101
	i32 1961813231, ; 291: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 248
	i32 1968388702, ; 292: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 175
	i32 1983156543, ; 293: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 268
	i32 1985761444, ; 294: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 193
	i32 2003115576, ; 295: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 278
	i32 2011961780, ; 296: System.Buffers.dll => 0x77ec19b4 => 8
	i32 2019465201, ; 297: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 233
	i32 2031763787, ; 298: Xamarin.Android.Glide => 0x791a414b => 190
	i32 2045470958, ; 299: System.Private.Xml => 0x79eb68ee => 89
	i32 2055257422, ; 300: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 228
	i32 2060060697, ; 301: System.Windows.dll => 0x7aca0819 => 155
	i32 2066184531, ; 302: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 277
	i32 2070888862, ; 303: System.Diagnostics.TraceSource => 0x7b6f419e => 34
	i32 2079903147, ; 304: System.Runtime.dll => 0x7bf8cdab => 117
	i32 2090596640, ; 305: System.Numerics.Vectors => 0x7c9bf920 => 83
	i32 2127167465, ; 306: System.Console => 0x7ec9ffe9 => 21
	i32 2140126677, ; 307: AIpaca_App.dll => 0x7f8fbdd5 => 1
	i32 2142473426, ; 308: System.Collections.Specialized => 0x7fb38cd2 => 12
	i32 2143790110, ; 309: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 163
	i32 2146852085, ; 310: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 4
	i32 2159891885, ; 311: Microsoft.Maui => 0x80bd55ad => 187
	i32 2169148018, ; 312: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 285
	i32 2181898931, ; 313: Microsoft.Extensions.Options.dll => 0x820d22b3 => 182
	i32 2192057212, ; 314: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 180
	i32 2193016926, ; 315: System.ObjectModel.dll => 0x82b6c85e => 85
	i32 2201107256, ; 316: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 272
	i32 2201231467, ; 317: System.Net.Http => 0x8334206b => 65
	i32 2207618523, ; 318: it\Microsoft.Maui.Controls.resources => 0x839595db => 287
	i32 2217644978, ; 319: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 255
	i32 2222056684, ; 320: System.Threading.Tasks.Parallel => 0x8471e4ec => 144
	i32 2244775296, ; 321: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 237
	i32 2252106437, ; 322: System.Xml.Serialization.dll => 0x863c6ac5 => 158
	i32 2256313426, ; 323: System.Globalization.Extensions => 0x867c9c52 => 42
	i32 2265110946, ; 324: System.Security.AccessControl.dll => 0x8702d9a2 => 118
	i32 2266799131, ; 325: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 176
	i32 2267999099, ; 326: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 192
	i32 2279755925, ; 327: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 244
	i32 2293034957, ; 328: System.ServiceModel.Web.dll => 0x88acefcd => 132
	i32 2295906218, ; 329: System.Net.Sockets => 0x88d8bfaa => 76
	i32 2298471582, ; 330: System.Net.Mail => 0x88ffe49e => 67
	i32 2303942373, ; 331: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 291
	i32 2305521784, ; 332: System.Private.CoreLib.dll => 0x896b7878 => 173
	i32 2315684594, ; 333: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 196
	i32 2320631194, ; 334: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 144
	i32 2340441535, ; 335: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 107
	i32 2344264397, ; 336: System.ValueTuple => 0x8bbaa2cd => 152
	i32 2353062107, ; 337: System.Net.Primitives => 0x8c40e0db => 71
	i32 2366048013, ; 338: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 285
	i32 2368005991, ; 339: System.Xml.ReaderWriter.dll => 0x8d24e767 => 157
	i32 2371007202, ; 340: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 175
	i32 2378619854, ; 341: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 122
	i32 2383496789, ; 342: System.Security.Principal.Windows.dll => 0x8e114655 => 128
	i32 2395872292, ; 343: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 286
	i32 2401565422, ; 344: System.Web.HttpUtility => 0x8f24faee => 153
	i32 2403452196, ; 345: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 219
	i32 2421380589, ; 346: System.Threading.Tasks.Dataflow => 0x905355ed => 142
	i32 2423080555, ; 347: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 206
	i32 2427813419, ; 348: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 283
	i32 2435356389, ; 349: System.Console.dll => 0x912896e5 => 21
	i32 2435904999, ; 350: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 15
	i32 2454642406, ; 351: System.Text.Encoding.dll => 0x924edee6 => 136
	i32 2458678730, ; 352: System.Net.Sockets.dll => 0x928c75ca => 76
	i32 2459001652, ; 353: System.Linq.Parallel.dll => 0x92916334 => 60
	i32 2465532216, ; 354: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 209
	i32 2471841756, ; 355: netstandard.dll => 0x93554fdc => 168
	i32 2475788418, ; 356: Java.Interop.dll => 0x93918882 => 169
	i32 2480646305, ; 357: Microsoft.Maui.Controls => 0x93dba8a1 => 185
	i32 2483903535, ; 358: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 16
	i32 2484371297, ; 359: System.Net.ServicePoint => 0x94147f61 => 75
	i32 2490993605, ; 360: System.AppContext.dll => 0x94798bc5 => 7
	i32 2501346920, ; 361: System.Data.DataSetExtensions => 0x95178668 => 24
	i32 2503351294, ; 362: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 289
	i32 2505896520, ; 363: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 231
	i32 2522472828, ; 364: Xamarin.Android.Glide.dll => 0x9659e17c => 190
	i32 2538310050, ; 365: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 92
	i32 2550873716, ; 366: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 284
	i32 2562349572, ; 367: Microsoft.CSharp => 0x98ba5a04 => 2
	i32 2570120770, ; 368: System.Text.Encodings.Web => 0x9930ee42 => 137
	i32 2576534780, ; 369: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 288
	i32 2581783588, ; 370: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 232
	i32 2581819634, ; 371: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 254
	i32 2585220780, ; 372: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 135
	i32 2585805581, ; 373: System.Net.Ping => 0x9a20430d => 70
	i32 2589602615, ; 374: System.Threading.ThreadPool => 0x9a5a3337 => 147
	i32 2593496499, ; 375: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 293
	i32 2605712449, ; 376: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 272
	i32 2615233544, ; 377: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 223
	i32 2616218305, ; 378: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 181
	i32 2617129537, ; 379: System.Private.Xml.dll => 0x9bfe3a41 => 89
	i32 2618712057, ; 380: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 97
	i32 2620871830, ; 381: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 213
	i32 2624644809, ; 382: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 218
	i32 2626831493, ; 383: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 288
	i32 2627185994, ; 384: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 32
	i32 2629843544, ; 385: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 46
	i32 2633051222, ; 386: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 227
	i32 2663391936, ; 387: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 192
	i32 2663698177, ; 388: System.Runtime.Loader => 0x9ec4cf01 => 110
	i32 2664396074, ; 389: System.Xml.XDocument.dll => 0x9ecf752a => 159
	i32 2665622720, ; 390: System.Drawing.Primitives => 0x9ee22cc0 => 36
	i32 2676780864, ; 391: System.Data.Common.dll => 0x9f8c6f40 => 23
	i32 2686887180, ; 392: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 115
	i32 2693849962, ; 393: System.IO.dll => 0xa090e36a => 58
	i32 2701096212, ; 394: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 252
	i32 2715334215, ; 395: System.Threading.Tasks.dll => 0xa1d8b647 => 145
	i32 2717744543, ; 396: System.Security.Claims => 0xa1fd7d9f => 119
	i32 2719963679, ; 397: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 121
	i32 2724373263, ; 398: System.Runtime.Numerics.dll => 0xa262a30f => 111
	i32 2732626843, ; 399: Xamarin.AndroidX.Activity => 0xa2e0939b => 194
	i32 2735172069, ; 400: System.Threading.Channels => 0xa30769e5 => 140
	i32 2737747696, ; 401: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 200
	i32 2740698338, ; 402: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 274
	i32 2740948882, ; 403: System.IO.Pipes.AccessControl => 0xa35f8f92 => 55
	i32 2748088231, ; 404: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 106
	i32 2752995522, ; 405: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 294
	i32 2758225723, ; 406: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 186
	i32 2764765095, ; 407: Microsoft.Maui.dll => 0xa4caf7a7 => 187
	i32 2765824710, ; 408: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 134
	i32 2770495804, ; 409: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 266
	i32 2778768386, ; 410: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 257
	i32 2779977773, ; 411: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 245
	i32 2785988530, ; 412: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 300
	i32 2788224221, ; 413: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 223
	i32 2801831435, ; 414: Microsoft.Maui.Graphics => 0xa7008e0b => 189
	i32 2803228030, ; 415: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 160
	i32 2810250172, ; 416: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 210
	i32 2819470561, ; 417: System.Xml.dll => 0xa80db4e1 => 164
	i32 2821205001, ; 418: System.ServiceProcess.dll => 0xa8282c09 => 133
	i32 2821294376, ; 419: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 245
	i32 2824502124, ; 420: System.Xml.XmlDocument => 0xa85a7b6c => 162
	i32 2838993487, ; 421: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 234
	i32 2849599387, ; 422: System.Threading.Overlapped.dll => 0xa9d96f9b => 141
	i32 2853208004, ; 423: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 257
	i32 2855708567, ; 424: Xamarin.AndroidX.Transition => 0xaa36a797 => 253
	i32 2861098320, ; 425: Mono.Android.Export.dll => 0xaa88e550 => 170
	i32 2861189240, ; 426: Microsoft.Maui.Essentials => 0xaa8a4878 => 188
	i32 2870099610, ; 427: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 195
	i32 2875164099, ; 428: Jsr305Binding.dll => 0xab5f85c3 => 262
	i32 2875220617, ; 429: System.Globalization.Calendars.dll => 0xab606289 => 41
	i32 2884993177, ; 430: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 221
	i32 2887636118, ; 431: System.Net.dll => 0xac1dd496 => 82
	i32 2899753641, ; 432: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 57
	i32 2900621748, ; 433: System.Dynamic.Runtime.dll => 0xace3f9b4 => 38
	i32 2901442782, ; 434: System.Reflection => 0xacf080de => 98
	i32 2905242038, ; 435: mscorlib.dll => 0xad2a79b6 => 167
	i32 2909740682, ; 436: System.Private.CoreLib => 0xad6f1e8a => 173
	i32 2916838712, ; 437: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 258
	i32 2919462931, ; 438: System.Numerics.Vectors.dll => 0xae037813 => 83
	i32 2921128767, ; 439: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 197
	i32 2936416060, ; 440: System.Resources.Reader => 0xaf06273c => 99
	i32 2940926066, ; 441: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 31
	i32 2942453041, ; 442: System.Xml.XPath.XDocument => 0xaf624531 => 160
	i32 2959614098, ; 443: System.ComponentModel.dll => 0xb0682092 => 19
	i32 2968338931, ; 444: System.Security.Principal.Windows => 0xb0ed41f3 => 128
	i32 2972252294, ; 445: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 120
	i32 2978675010, ; 446: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 217
	i32 2987532451, ; 447: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 248
	i32 2996846495, ; 448: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 230
	i32 3016983068, ; 449: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 250
	i32 3023353419, ; 450: WindowsBase.dll => 0xb434b64b => 166
	i32 3024354802, ; 451: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 225
	i32 3038032645, ; 452: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 307
	i32 3053864966, ; 453: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 280
	i32 3056245963, ; 454: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 247
	i32 3057625584, ; 455: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 238
	i32 3059408633, ; 456: Mono.Android.Runtime => 0xb65adef9 => 171
	i32 3059793426, ; 457: System.ComponentModel.Primitives => 0xb660be12 => 17
	i32 3075834255, ; 458: System.Threading.Tasks => 0xb755818f => 145
	i32 3090735792, ; 459: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 126
	i32 3099732863, ; 460: System.Security.Claims.dll => 0xb8c22b7f => 119
	i32 3103600923, ; 461: System.Formats.Asn1 => 0xb8fd311b => 39
	i32 3111772706, ; 462: System.Runtime.Serialization => 0xb979e222 => 116
	i32 3121463068, ; 463: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 48
	i32 3124832203, ; 464: System.Threading.Tasks.Extensions => 0xba4127cb => 143
	i32 3132293585, ; 465: System.Security.AccessControl => 0xbab301d1 => 118
	i32 3147165239, ; 466: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 35
	i32 3148237826, ; 467: GoogleGson.dll => 0xbba64c02 => 174
	i32 3159123045, ; 468: System.Reflection.Primitives.dll => 0xbc4c6465 => 96
	i32 3160747431, ; 469: System.IO.MemoryMappedFiles => 0xbc652da7 => 54
	i32 3178803400, ; 470: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 239
	i32 3192346100, ; 471: System.Security.SecureString => 0xbe4755f4 => 130
	i32 3193515020, ; 472: System.Web => 0xbe592c0c => 154
	i32 3204380047, ; 473: System.Data.dll => 0xbefef58f => 25
	i32 3209718065, ; 474: System.Xml.XmlDocument.dll => 0xbf506931 => 162
	i32 3211777861, ; 475: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 216
	i32 3220365878, ; 476: System.Threading => 0xbff2e236 => 149
	i32 3226221578, ; 477: System.Runtime.Handles.dll => 0xc04c3c0a => 105
	i32 3251039220, ; 478: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 90
	i32 3258312781, ; 479: Xamarin.AndroidX.CardView => 0xc235e84d => 204
	i32 3265493905, ; 480: System.Linq.Queryable.dll => 0xc2a37b91 => 61
	i32 3265893370, ; 481: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 143
	i32 3277815716, ; 482: System.Resources.Writer.dll => 0xc35f7fa4 => 101
	i32 3279906254, ; 483: Microsoft.Win32.Registry.dll => 0xc37f65ce => 6
	i32 3280506390, ; 484: System.ComponentModel.Annotations.dll => 0xc3888e16 => 14
	i32 3290767353, ; 485: System.Security.Cryptography.Encoding => 0xc4251ff9 => 123
	i32 3299363146, ; 486: System.Text.Encoding => 0xc4a8494a => 136
	i32 3303498502, ; 487: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 29
	i32 3305363605, ; 488: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 280
	i32 3316684772, ; 489: System.Net.Requests.dll => 0xc5b097e4 => 73
	i32 3317135071, ; 490: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 214
	i32 3317144872, ; 491: System.Data => 0xc5b79d28 => 25
	i32 3340431453, ; 492: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 202
	i32 3345895724, ; 493: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 243
	i32 3346324047, ; 494: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 240
	i32 3357674450, ; 495: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 297
	i32 3358260929, ; 496: System.Text.Json => 0xc82afec1 => 138
	i32 3362336904, ; 497: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 195
	i32 3362522851, ; 498: Xamarin.AndroidX.Core => 0xc86c06e3 => 211
	i32 3366347497, ; 499: Java.Interop => 0xc8a662e9 => 169
	i32 3374999561, ; 500: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 244
	i32 3381016424, ; 501: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 276
	i32 3395150330, ; 502: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 102
	i32 3403906625, ; 503: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 124
	i32 3405233483, ; 504: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 215
	i32 3428513518, ; 505: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 177
	i32 3429136800, ; 506: System.Xml => 0xcc6479a0 => 164
	i32 3430777524, ; 507: netstandard => 0xcc7d82b4 => 168
	i32 3441283291, ; 508: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 218
	i32 3445260447, ; 509: System.Formats.Tar => 0xcd5a809f => 40
	i32 3452344032, ; 510: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 184
	i32 3458724246, ; 511: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 295
	i32 3471940407, ; 512: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 18
	i32 3476120550, ; 513: Mono.Android => 0xcf3163e6 => 172
	i32 3484440000, ; 514: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 296
	i32 3485117614, ; 515: System.Text.Json.dll => 0xcfbaacae => 138
	i32 3486566296, ; 516: System.Transactions => 0xcfd0c798 => 151
	i32 3493954962, ; 517: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 207
	i32 3509114376, ; 518: System.Xml.Linq => 0xd128d608 => 156
	i32 3515174580, ; 519: System.Security.dll => 0xd1854eb4 => 131
	i32 3530912306, ; 520: System.Configuration => 0xd2757232 => 20
	i32 3539954161, ; 521: System.Net.HttpListener => 0xd2ff69f1 => 66
	i32 3560100363, ; 522: System.Threading.Timer => 0xd432d20b => 148
	i32 3570554715, ; 523: System.IO.FileSystem.AccessControl => 0xd4d2575b => 48
	i32 3580758918, ; 524: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 304
	i32 3592228221, ; 525: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 306
	i32 3597029428, ; 526: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 193
	i32 3598340787, ; 527: System.Net.WebSockets.Client => 0xd67a52b3 => 80
	i32 3608519521, ; 528: System.Linq.dll => 0xd715a361 => 62
	i32 3624195450, ; 529: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 107
	i32 3627220390, ; 530: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 242
	i32 3633644679, ; 531: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 197
	i32 3638274909, ; 532: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 50
	i32 3641597786, ; 533: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 228
	i32 3643446276, ; 534: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 301
	i32 3643854240, ; 535: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 239
	i32 3645089577, ; 536: System.ComponentModel.DataAnnotations => 0xd943a729 => 15
	i32 3657292374, ; 537: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 176
	i32 3660523487, ; 538: System.Net.NetworkInformation => 0xda2f27df => 69
	i32 3672681054, ; 539: Mono.Android.dll => 0xdae8aa5e => 172
	i32 3682565725, ; 540: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 203
	i32 3684561358, ; 541: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 207
	i32 3700866549, ; 542: System.Net.WebProxy.dll => 0xdc96bdf5 => 79
	i32 3706696989, ; 543: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 212
	i32 3716563718, ; 544: System.Runtime.Intrinsics => 0xdd864306 => 109
	i32 3718780102, ; 545: Xamarin.AndroidX.Annotation => 0xdda814c6 => 196
	i32 3724971120, ; 546: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 238
	i32 3732100267, ; 547: System.Net.NameResolution => 0xde7354ab => 68
	i32 3737834244, ; 548: System.Net.Http.Json.dll => 0xdecad304 => 64
	i32 3748608112, ; 549: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 28
	i32 3751444290, ; 550: System.Xml.XPath => 0xdf9a7f42 => 161
	i32 3751619990, ; 551: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 276
	i32 3786282454, ; 552: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 205
	i32 3792276235, ; 553: System.Collections.NonGeneric => 0xe2098b0b => 11
	i32 3800979733, ; 554: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 184
	i32 3802395368, ; 555: System.Collections.Specialized.dll => 0xe2a3f2e8 => 12
	i32 3819260425, ; 556: System.Net.WebProxy => 0xe3a54a09 => 79
	i32 3823082795, ; 557: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3829621856, ; 558: System.Numerics.dll => 0xe4436460 => 84
	i32 3841636137, ; 559: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 178
	i32 3844307129, ; 560: System.Net.Mail.dll => 0xe52378b9 => 67
	i32 3849253459, ; 561: System.Runtime.InteropServices.dll => 0xe56ef253 => 108
	i32 3870376305, ; 562: System.Net.HttpListener.dll => 0xe6b14171 => 66
	i32 3873536506, ; 563: System.Security.Principal => 0xe6e179fa => 129
	i32 3875112723, ; 564: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 123
	i32 3885497537, ; 565: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 78
	i32 3885922214, ; 566: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 253
	i32 3888767677, ; 567: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 243
	i32 3896106733, ; 568: System.Collections.Concurrent.dll => 0xe839deed => 9
	i32 3896760992, ; 569: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 211
	i32 3901907137, ; 570: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 3
	i32 3920221145, ; 571: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 292
	i32 3920810846, ; 572: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 45
	i32 3921031405, ; 573: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 256
	i32 3928044579, ; 574: System.Xml.ReaderWriter => 0xea213423 => 157
	i32 3930554604, ; 575: System.Security.Principal.dll => 0xea4780ec => 129
	i32 3931092270, ; 576: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 241
	i32 3945713374, ; 577: System.Data.DataSetExtensions.dll => 0xeb2ecede => 24
	i32 3953953790, ; 578: System.Text.Encoding.CodePages => 0xebac8bfe => 134
	i32 3955647286, ; 579: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 199
	i32 3959773229, ; 580: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 230
	i32 4003436829, ; 581: System.Diagnostics.Process.dll => 0xee9f991d => 30
	i32 4015948917, ; 582: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 198
	i32 4025784931, ; 583: System.Memory => 0xeff49a63 => 63
	i32 4046471985, ; 584: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 186
	i32 4054681211, ; 585: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 91
	i32 4068434129, ; 586: System.Private.Xml.Linq.dll => 0xf27f60d1 => 88
	i32 4073602200, ; 587: System.Threading.dll => 0xf2ce3c98 => 149
	i32 4091086043, ; 588: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 278
	i32 4094352644, ; 589: Microsoft.Maui.Essentials.dll => 0xf40add04 => 188
	i32 4099507663, ; 590: System.Drawing.dll => 0xf45985cf => 37
	i32 4100113165, ; 591: System.Private.Uri => 0xf462c30d => 87
	i32 4101593132, ; 592: Xamarin.AndroidX.Emoji2 => 0xf479582c => 219
	i32 4103439459, ; 593: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 302
	i32 4126470640, ; 594: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 177
	i32 4127667938, ; 595: System.IO.FileSystem.Watcher => 0xf60736e2 => 51
	i32 4130442656, ; 596: System.AppContext => 0xf6318da0 => 7
	i32 4147896353, ; 597: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 91
	i32 4150914736, ; 598: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 302
	i32 4151237749, ; 599: System.Core => 0xf76edc75 => 22
	i32 4159265925, ; 600: System.Xml.XmlSerializer => 0xf7e95c85 => 163
	i32 4161255271, ; 601: System.Reflection.TypeExtensions => 0xf807b767 => 97
	i32 4164802419, ; 602: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 51
	i32 4181436372, ; 603: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 114
	i32 4182413190, ; 604: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 235
	i32 4185676441, ; 605: System.Security => 0xf97c5a99 => 131
	i32 4196529839, ; 606: System.Net.WebClient.dll => 0xfa21f6af => 77
	i32 4213026141, ; 607: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 28
	i32 4249188766, ; 608: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 291
	i32 4256097574, ; 609: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 212
	i32 4258378803, ; 610: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 234
	i32 4260525087, ; 611: System.Buffers => 0xfdf2741f => 8
	i32 4271975918, ; 612: Microsoft.Maui.Controls.dll => 0xfea12dee => 185
	i32 4274976490, ; 613: System.Runtime.Numerics => 0xfecef6ea => 111
	i32 4292120959, ; 614: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 235
	i32 4294763496 ; 615: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 221
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [616 x i32] [
	i32 69, ; 0
	i32 68, ; 1
	i32 109, ; 2
	i32 231, ; 3
	i32 265, ; 4
	i32 49, ; 5
	i32 273, ; 6
	i32 81, ; 7
	i32 282, ; 8
	i32 146, ; 9
	i32 31, ; 10
	i32 306, ; 11
	i32 125, ; 12
	i32 189, ; 13
	i32 103, ; 14
	i32 290, ; 15
	i32 249, ; 16
	i32 108, ; 17
	i32 249, ; 18
	i32 140, ; 19
	i32 269, ; 20
	i32 305, ; 21
	i32 298, ; 22
	i32 78, ; 23
	i32 125, ; 24
	i32 14, ; 25
	i32 205, ; 26
	i32 133, ; 27
	i32 251, ; 28
	i32 152, ; 29
	i32 19, ; 30
	i32 203, ; 31
	i32 27, ; 32
	i32 0, ; 33
	i32 225, ; 34
	i32 2, ; 35
	i32 60, ; 36
	i32 43, ; 37
	i32 92, ; 38
	i32 208, ; 39
	i32 148, ; 40
	i32 227, ; 41
	i32 224, ; 42
	i32 55, ; 43
	i32 70, ; 44
	i32 303, ; 45
	i32 194, ; 46
	i32 84, ; 47
	i32 281, ; 48
	i32 226, ; 49
	i32 132, ; 50
	i32 56, ; 51
	i32 150, ; 52
	i32 75, ; 53
	i32 146, ; 54
	i32 63, ; 55
	i32 147, ; 56
	i32 307, ; 57
	i32 166, ; 58
	i32 301, ; 59
	i32 209, ; 60
	i32 13, ; 61
	i32 222, ; 62
	i32 126, ; 63
	i32 153, ; 64
	i32 114, ; 65
	i32 167, ; 66
	i32 165, ; 67
	i32 224, ; 68
	i32 237, ; 69
	i32 279, ; 70
	i32 85, ; 71
	i32 183, ; 72
	i32 151, ; 73
	i32 269, ; 74
	i32 61, ; 75
	i32 300, ; 76
	i32 179, ; 77
	i32 52, ; 78
	i32 104, ; 79
	i32 115, ; 80
	i32 41, ; 81
	i32 262, ; 82
	i32 260, ; 83
	i32 121, ; 84
	i32 53, ; 85
	i32 45, ; 86
	i32 120, ; 87
	i32 214, ; 88
	i32 292, ; 89
	i32 220, ; 90
	i32 82, ; 91
	i32 137, ; 92
	i32 256, ; 93
	i32 201, ; 94
	i32 9, ; 95
	i32 0, ; 96
	i32 74, ; 97
	i32 156, ; 98
	i32 271, ; 99
	i32 155, ; 100
	i32 93, ; 101
	i32 266, ; 102
	i32 46, ; 103
	i32 270, ; 104
	i32 110, ; 105
	i32 130, ; 106
	i32 26, ; 107
	i32 191, ; 108
	i32 73, ; 109
	i32 56, ; 110
	i32 47, ; 111
	i32 298, ; 112
	i32 182, ; 113
	i32 215, ; 114
	i32 23, ; 115
	i32 229, ; 116
	i32 87, ; 117
	i32 44, ; 118
	i32 161, ; 119
	i32 72, ; 120
	i32 242, ; 121
	i32 283, ; 122
	i32 4, ; 123
	i32 43, ; 124
	i32 64, ; 125
	i32 297, ; 126
	i32 17, ; 127
	i32 54, ; 128
	i32 294, ; 129
	i32 265, ; 130
	i32 106, ; 131
	i32 270, ; 132
	i32 287, ; 133
	i32 263, ; 134
	i32 226, ; 135
	i32 35, ; 136
	i32 159, ; 137
	i32 86, ; 138
	i32 33, ; 139
	i32 296, ; 140
	i32 13, ; 141
	i32 52, ; 142
	i32 57, ; 143
	i32 246, ; 144
	i32 37, ; 145
	i32 178, ; 146
	i32 264, ; 147
	i32 199, ; 148
	i32 36, ; 149
	i32 277, ; 150
	i32 59, ; 151
	i32 233, ; 152
	i32 174, ; 153
	i32 18, ; 154
	i32 267, ; 155
	i32 165, ; 156
	i32 299, ; 157
	i32 293, ; 158
	i32 289, ; 159
	i32 232, ; 160
	i32 181, ; 161
	i32 259, ; 162
	i32 295, ; 163
	i32 154, ; 164
	i32 255, ; 165
	i32 240, ; 166
	i32 201, ; 167
	i32 30, ; 168
	i32 53, ; 169
	i32 260, ; 170
	i32 6, ; 171
	i32 275, ; 172
	i32 250, ; 173
	i32 254, ; 174
	i32 206, ; 175
	i32 271, ; 176
	i32 198, ; 177
	i32 217, ; 178
	i32 284, ; 179
	i32 86, ; 180
	i32 259, ; 181
	i32 62, ; 182
	i32 113, ; 183
	i32 304, ; 184
	i32 58, ; 185
	i32 305, ; 186
	i32 246, ; 187
	i32 100, ; 188
	i32 20, ; 189
	i32 210, ; 190
	i32 112, ; 191
	i32 102, ; 192
	i32 103, ; 193
	i32 273, ; 194
	i32 105, ; 195
	i32 263, ; 196
	i32 72, ; 197
	i32 39, ; 198
	i32 33, ; 199
	i32 104, ; 200
	i32 74, ; 201
	i32 279, ; 202
	i32 10, ; 203
	i32 124, ; 204
	i32 47, ; 205
	i32 200, ; 206
	i32 183, ; 207
	i32 10, ; 208
	i32 44, ; 209
	i32 5, ; 210
	i32 247, ; 211
	i32 303, ; 212
	i32 32, ; 213
	i32 139, ; 214
	i32 93, ; 215
	i32 94, ; 216
	i32 50, ; 217
	i32 142, ; 218
	i32 113, ; 219
	i32 141, ; 220
	i32 216, ; 221
	i32 116, ; 222
	i32 264, ; 223
	i32 158, ; 224
	i32 77, ; 225
	i32 80, ; 226
	i32 236, ; 227
	i32 38, ; 228
	i32 258, ; 229
	i32 220, ; 230
	i32 213, ; 231
	i32 65, ; 232
	i32 139, ; 233
	i32 16, ; 234
	i32 117, ; 235
	i32 252, ; 236
	i32 261, ; 237
	i32 208, ; 238
	i32 49, ; 239
	i32 71, ; 240
	i32 81, ; 241
	i32 127, ; 242
	i32 95, ; 243
	i32 122, ; 244
	i32 268, ; 245
	i32 27, ; 246
	i32 229, ; 247
	i32 98, ; 248
	i32 29, ; 249
	i32 204, ; 250
	i32 274, ; 251
	i32 150, ; 252
	i32 170, ; 253
	i32 5, ; 254
	i32 99, ; 255
	i32 34, ; 256
	i32 94, ; 257
	i32 251, ; 258
	i32 179, ; 259
	i32 22, ; 260
	i32 42, ; 261
	i32 171, ; 262
	i32 290, ; 263
	i32 222, ; 264
	i32 1, ; 265
	i32 282, ; 266
	i32 236, ; 267
	i32 267, ; 268
	i32 261, ; 269
	i32 241, ; 270
	i32 3, ; 271
	i32 135, ; 272
	i32 112, ; 273
	i32 180, ; 274
	i32 191, ; 275
	i32 299, ; 276
	i32 59, ; 277
	i32 96, ; 278
	i32 281, ; 279
	i32 40, ; 280
	i32 202, ; 281
	i32 26, ; 282
	i32 95, ; 283
	i32 275, ; 284
	i32 90, ; 285
	i32 100, ; 286
	i32 11, ; 287
	i32 88, ; 288
	i32 286, ; 289
	i32 101, ; 290
	i32 248, ; 291
	i32 175, ; 292
	i32 268, ; 293
	i32 193, ; 294
	i32 278, ; 295
	i32 8, ; 296
	i32 233, ; 297
	i32 190, ; 298
	i32 89, ; 299
	i32 228, ; 300
	i32 155, ; 301
	i32 277, ; 302
	i32 34, ; 303
	i32 117, ; 304
	i32 83, ; 305
	i32 21, ; 306
	i32 1, ; 307
	i32 12, ; 308
	i32 163, ; 309
	i32 4, ; 310
	i32 187, ; 311
	i32 285, ; 312
	i32 182, ; 313
	i32 180, ; 314
	i32 85, ; 315
	i32 272, ; 316
	i32 65, ; 317
	i32 287, ; 318
	i32 255, ; 319
	i32 144, ; 320
	i32 237, ; 321
	i32 158, ; 322
	i32 42, ; 323
	i32 118, ; 324
	i32 176, ; 325
	i32 192, ; 326
	i32 244, ; 327
	i32 132, ; 328
	i32 76, ; 329
	i32 67, ; 330
	i32 291, ; 331
	i32 173, ; 332
	i32 196, ; 333
	i32 144, ; 334
	i32 107, ; 335
	i32 152, ; 336
	i32 71, ; 337
	i32 285, ; 338
	i32 157, ; 339
	i32 175, ; 340
	i32 122, ; 341
	i32 128, ; 342
	i32 286, ; 343
	i32 153, ; 344
	i32 219, ; 345
	i32 142, ; 346
	i32 206, ; 347
	i32 283, ; 348
	i32 21, ; 349
	i32 15, ; 350
	i32 136, ; 351
	i32 76, ; 352
	i32 60, ; 353
	i32 209, ; 354
	i32 168, ; 355
	i32 169, ; 356
	i32 185, ; 357
	i32 16, ; 358
	i32 75, ; 359
	i32 7, ; 360
	i32 24, ; 361
	i32 289, ; 362
	i32 231, ; 363
	i32 190, ; 364
	i32 92, ; 365
	i32 284, ; 366
	i32 2, ; 367
	i32 137, ; 368
	i32 288, ; 369
	i32 232, ; 370
	i32 254, ; 371
	i32 135, ; 372
	i32 70, ; 373
	i32 147, ; 374
	i32 293, ; 375
	i32 272, ; 376
	i32 223, ; 377
	i32 181, ; 378
	i32 89, ; 379
	i32 97, ; 380
	i32 213, ; 381
	i32 218, ; 382
	i32 288, ; 383
	i32 32, ; 384
	i32 46, ; 385
	i32 227, ; 386
	i32 192, ; 387
	i32 110, ; 388
	i32 159, ; 389
	i32 36, ; 390
	i32 23, ; 391
	i32 115, ; 392
	i32 58, ; 393
	i32 252, ; 394
	i32 145, ; 395
	i32 119, ; 396
	i32 121, ; 397
	i32 111, ; 398
	i32 194, ; 399
	i32 140, ; 400
	i32 200, ; 401
	i32 274, ; 402
	i32 55, ; 403
	i32 106, ; 404
	i32 294, ; 405
	i32 186, ; 406
	i32 187, ; 407
	i32 134, ; 408
	i32 266, ; 409
	i32 257, ; 410
	i32 245, ; 411
	i32 300, ; 412
	i32 223, ; 413
	i32 189, ; 414
	i32 160, ; 415
	i32 210, ; 416
	i32 164, ; 417
	i32 133, ; 418
	i32 245, ; 419
	i32 162, ; 420
	i32 234, ; 421
	i32 141, ; 422
	i32 257, ; 423
	i32 253, ; 424
	i32 170, ; 425
	i32 188, ; 426
	i32 195, ; 427
	i32 262, ; 428
	i32 41, ; 429
	i32 221, ; 430
	i32 82, ; 431
	i32 57, ; 432
	i32 38, ; 433
	i32 98, ; 434
	i32 167, ; 435
	i32 173, ; 436
	i32 258, ; 437
	i32 83, ; 438
	i32 197, ; 439
	i32 99, ; 440
	i32 31, ; 441
	i32 160, ; 442
	i32 19, ; 443
	i32 128, ; 444
	i32 120, ; 445
	i32 217, ; 446
	i32 248, ; 447
	i32 230, ; 448
	i32 250, ; 449
	i32 166, ; 450
	i32 225, ; 451
	i32 307, ; 452
	i32 280, ; 453
	i32 247, ; 454
	i32 238, ; 455
	i32 171, ; 456
	i32 17, ; 457
	i32 145, ; 458
	i32 126, ; 459
	i32 119, ; 460
	i32 39, ; 461
	i32 116, ; 462
	i32 48, ; 463
	i32 143, ; 464
	i32 118, ; 465
	i32 35, ; 466
	i32 174, ; 467
	i32 96, ; 468
	i32 54, ; 469
	i32 239, ; 470
	i32 130, ; 471
	i32 154, ; 472
	i32 25, ; 473
	i32 162, ; 474
	i32 216, ; 475
	i32 149, ; 476
	i32 105, ; 477
	i32 90, ; 478
	i32 204, ; 479
	i32 61, ; 480
	i32 143, ; 481
	i32 101, ; 482
	i32 6, ; 483
	i32 14, ; 484
	i32 123, ; 485
	i32 136, ; 486
	i32 29, ; 487
	i32 280, ; 488
	i32 73, ; 489
	i32 214, ; 490
	i32 25, ; 491
	i32 202, ; 492
	i32 243, ; 493
	i32 240, ; 494
	i32 297, ; 495
	i32 138, ; 496
	i32 195, ; 497
	i32 211, ; 498
	i32 169, ; 499
	i32 244, ; 500
	i32 276, ; 501
	i32 102, ; 502
	i32 124, ; 503
	i32 215, ; 504
	i32 177, ; 505
	i32 164, ; 506
	i32 168, ; 507
	i32 218, ; 508
	i32 40, ; 509
	i32 184, ; 510
	i32 295, ; 511
	i32 18, ; 512
	i32 172, ; 513
	i32 296, ; 514
	i32 138, ; 515
	i32 151, ; 516
	i32 207, ; 517
	i32 156, ; 518
	i32 131, ; 519
	i32 20, ; 520
	i32 66, ; 521
	i32 148, ; 522
	i32 48, ; 523
	i32 304, ; 524
	i32 306, ; 525
	i32 193, ; 526
	i32 80, ; 527
	i32 62, ; 528
	i32 107, ; 529
	i32 242, ; 530
	i32 197, ; 531
	i32 50, ; 532
	i32 228, ; 533
	i32 301, ; 534
	i32 239, ; 535
	i32 15, ; 536
	i32 176, ; 537
	i32 69, ; 538
	i32 172, ; 539
	i32 203, ; 540
	i32 207, ; 541
	i32 79, ; 542
	i32 212, ; 543
	i32 109, ; 544
	i32 196, ; 545
	i32 238, ; 546
	i32 68, ; 547
	i32 64, ; 548
	i32 28, ; 549
	i32 161, ; 550
	i32 276, ; 551
	i32 205, ; 552
	i32 11, ; 553
	i32 184, ; 554
	i32 12, ; 555
	i32 79, ; 556
	i32 127, ; 557
	i32 84, ; 558
	i32 178, ; 559
	i32 67, ; 560
	i32 108, ; 561
	i32 66, ; 562
	i32 129, ; 563
	i32 123, ; 564
	i32 78, ; 565
	i32 253, ; 566
	i32 243, ; 567
	i32 9, ; 568
	i32 211, ; 569
	i32 3, ; 570
	i32 292, ; 571
	i32 45, ; 572
	i32 256, ; 573
	i32 157, ; 574
	i32 129, ; 575
	i32 241, ; 576
	i32 24, ; 577
	i32 134, ; 578
	i32 199, ; 579
	i32 230, ; 580
	i32 30, ; 581
	i32 198, ; 582
	i32 63, ; 583
	i32 186, ; 584
	i32 91, ; 585
	i32 88, ; 586
	i32 149, ; 587
	i32 278, ; 588
	i32 188, ; 589
	i32 37, ; 590
	i32 87, ; 591
	i32 219, ; 592
	i32 302, ; 593
	i32 177, ; 594
	i32 51, ; 595
	i32 7, ; 596
	i32 91, ; 597
	i32 302, ; 598
	i32 22, ; 599
	i32 163, ; 600
	i32 97, ; 601
	i32 51, ; 602
	i32 114, ; 603
	i32 235, ; 604
	i32 131, ; 605
	i32 77, ; 606
	i32 28, ; 607
	i32 291, ; 608
	i32 212, ; 609
	i32 234, ; 610
	i32 8, ; 611
	i32 185, ; 612
	i32 111, ; 613
	i32 235, ; 614
	i32 221 ; 615
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ f1b7113872c8db3dfee70d11925e81bb752dc8d0"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
