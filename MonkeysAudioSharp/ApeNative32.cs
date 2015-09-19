using System;
using System.Runtime.InteropServices;

namespace MonkeysAudioSharp
{
	/// <summary>
	/// P/Invoke class for interfacing with MACDll.
	/// </summary>
	public static class ApeNative32
	{
		#region P/Invoke Methods

		private const string DllName = "MACDll32";

		#region Misc
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int GetVersionNumber();
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int TagFileSimple(string filename, string artist, string album, string title, string comment, string genre, string year, string track, [MarshalAs(UnmanagedType.Bool)] bool clearFirst, [MarshalAs(UnmanagedType.Bool)] bool useOldId3);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int GetID3Tag(string filename, IntPtr id3Tag);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int RemoveTag(string filename);

		#endregion

		#region Decompression
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr c_APEDecompress_Create(string filename, out int errorCode);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern IntPtr c_APEDecompress_CreateW(string filename, out int errorCode);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern void c_APEDecompress_Destroy(IntPtr handle);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APEDecompress_GetData(IntPtr handle, sbyte[] buffer, int numBlocks, out int blocksRetrieved);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APEDecompress_Seek(IntPtr handle, int blockOffset);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr c_APEDecompress_GetInfo(IntPtr handle, DecompressInfo field, int nParam1, int nParam2);

		#endregion

		#region Compression
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr c_APECompress_Create(out int errorCode);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern void c_APECompress_Destroy(IntPtr handle);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int c_APECompress_Start(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern int c_APECompress_StartW(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_AddData(IntPtr handle, byte[] data, int numBytes);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_GetBufferBytesAvailable(IntPtr handle);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr c_APECompress_LockBuffer(IntPtr handle, out int bytesAvailable);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_UnlockBuffer(IntPtr handle, int bytesAdded, [MarshalAs(UnmanagedType.Bool)]bool process);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_Finish(IntPtr handle, IntPtr terminatingData, int terminatingBytes, int wavTerminatingBytes);
        
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_Kill(IntPtr handle);

		#endregion

		#endregion
	}
}
