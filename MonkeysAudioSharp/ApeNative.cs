using System;
using System.Runtime.InteropServices;

namespace MonkeysAudioSharp
{
	/// <summary>
	/// P/Invoke class for interfacing with MACDll.
	/// </summary>
	public static class ApeNative
	{
		#region Utility Methods

		/// <summary>
		/// Same as <see cref="c_APEDecompress_GetInfo"/>, but used
		/// in the case where you don't care about the params and want it as an int.
		/// </summary>
		/// <param name="handle">The handle to the current decompresser instance.</param>
		/// <param name="field">The info to get.</param>
		/// <returns>Int representing whatever info is requested from <see cref="DecompressInfo"/></returns>
		public static int Decompress_GetInfoInt(IntPtr handle, DecompressInfo field)
		{
			return c_APEDecompress_GetInfo(handle, field, 0, 0).ToInt32();
		}

		/// <summary>
		/// Same as <see cref="c_APEDecompress_GetInfo"/>, but used
		/// in the case where you don't care about the params and want it as an int.
		/// </summary>
		/// <param name="handle">The handle to the current decompresser instance.</param>
		/// <param name="field">The info to get.</param>
		/// <param name="param1">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
		/// <param name="param2">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
		/// <returns>Int representing whatever info is requested from <see cref="DecompressInfo"/></returns>
		public static int Decompress_GetInfoInt(IntPtr handle, DecompressInfo field, int param1, int param2)
		{
			return c_APEDecompress_GetInfo(handle, field, param1, param2).ToInt32();
		}

		#endregion

		#region P/Invoke Methods

		private const string DllName = "MACDll";

		#region Misc

		/// <summary>
		/// Gets the codec version number.
		/// </summary>
		/// <returns>The version number of the codec</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int GetVersionNumber();

		/// <summary>
		/// Adds an ID3 tag to the given APE file.
		/// </summary>
		/// <param name="filename">File path to the APE file.</param>
		/// <param name="artist">Artist name</param>
		/// <param name="album">Album name</param>
		/// <param name="title">Song title</param>
		/// <param name="comment">ID3 comment</param>
		/// <param name="genre">Song genre</param>
		/// <param name="year">Year of release</param>
		/// <param name="track">Track number</param>
		/// <param name="clearFirst">Clear the tag (if one exists) before writing the new one.</param>
		/// <param name="useOldId3">Whether or not to use an old version of ID3.</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int TagFileSimple(string filename, string artist, string album, string title, string comment, string genre, string year, string track, [MarshalAs(UnmanagedType.Bool)] bool clearFirst, [MarshalAs(UnmanagedType.Bool)] bool useOldId3);

		/// <summary>
		/// Gets the ID3 tag within the given APE file.
		/// </summary>
		/// <param name="filename">Path to the APE file.</param>
		/// <param name="id3Tag">The retrieved ID3 tag</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int GetID3Tag(string filename, IntPtr id3Tag);

		/// <summary>
		/// Removes the ID3 tag from the given APE file if it exists.
		/// </summary>
		/// <param name="filename">Path to the APE file.</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int RemoveTag(string filename);

		#endregion

		#region Decompression

		#region Info query enum
		/// <summary>
		/// Used when querying for information.
		/// </summary>
		/// <remarks>
		/// The distinction between APE_INFO_XXXX and APE_DECOMPRESS_XXXX is that the first is querying the APE
		/// information engine, and the other is querying the decompressor, and since the decompressor can be
		/// a range of an APE file (for APL), differences will arise.  Typically, use the APE_DECOMPRESS_XXXX
		/// fields when querying for info about the length, etc. so APL will work properly. 
		/// (i.e. (APE_INFO_TOTAL_BLOCKS != APE_DECOMPRESS_TOTAL_BLOCKS) for APL files)
		/// </remarks>
		public enum DecompressInfo
		{
			/// <summary>Version of the APE file * 1000 (3.93 = 3930) [ignored, ignored]</summary>
			FileVersion         = 1000,

			/// <summary>Compression level of the APE file [ignored, ignored]</summary>
			CompressionLevel    = 1001,

			/// <summary>Format flags of the APE file [ignored, ignored]</summary>
			FormatFlags         = 1002,

			/// <summary>Sample rate (Hz) [ignored, ignored]</summary>
			SampleRate          = 1003,

			/// <summary>Bits per sample [ignored, ignored]</summary>
			BitsPerSample       = 1004,

			/// <summary>Number of bytes per sample [ignored, ignored]</summary>
			BytesPerSample      = 1005,

			/// <summary>Number of channels [ignored, ignored]</summary>
			Channels            = 1006,

			/// <summary>Block alignment [ignored, ignored]</summary>
			BlockAlignment      = 1007,

			/// <summary>Number of blocks in a frame (frames are used internally)  [ignored, ignored]</summary>
			BlocksPerFrame      = 1008,

			/// <summary>Blocks in the final frame (frames are used internally) [ignored, ignored]</summary>
			FinalFrameBlocks    = 1009,

			/// <summary>Total number frames (frames are used internally) [ignored, ignored]</summary>
			TotalFrames         = 1010,

			/// <summary>Header bytes of the decompressed WAV [ignored, ignored]</summary>
			WavHeaderBytes      = 1011,

			/// <summary>Terminating bytes of the decompressed WAV [ignored, ignored]</summary>
			WavTerminatingBytes = 1012,

			/// <summary>Data bytes of the decompressed WAV [ignored, ignored]</summary>
			WavDataBytes        = 1013,

			/// <summary>Total bytes of the decompressed WAV [ignored, ignored]</summary>
			WavTotalBytes       = 1014,

			/// <summary>Total bytes of the APE file [ignored, ignored]</summary>
			ApeTotalBytes       = 1015,

			/// <summary>Total blocks of audio data [ignored, ignored]</summary>
			TotalBlocks         = 1016,

			/// <summary>Length in ms (1 sec = 1000 ms) [ignored, ignored]</summary>
			LengthInMs          = 1017,

			/// <summary>Average bitrate of the APE [ignored, ignored]</summary>
			AverageBitrate      = 1018,

			/// <summary>Bitrate of specified APE frame [frame index, ignored]</summary>
			FrameBitrate        = 1019,

			/// <summary>Bitrate of the decompressed WAV [ignored, ignored]</summary>
			DecompressedBitrate = 1020,

			/// <summary>Peak audio level (obsolete) (-1 is unknown) [ignored, ignored]</summary>
			PeakLevel           = 1021,

			/// <summary>Bit offset [frame index, ignored]</summary>
			SeekBit             = 1022,

			/// <summary>Byte offset [frame index, ignored]</summary>
			SeekByte            = 1023,

			/// <summary>Error code [buffer *, max bytes]</summary>
			WavHeaderData       = 1024,

			/// <summary>Error code [buffer *, max bytes]</summary>
			WavTerminatingData  = 1025,

			/// <summary>Error code [waveformatex *, ignored]</summary>
			WaveFormatEx        = 1026,

			/// <summary>I/O source (CIO *) [ignored, ignored]</summary>
			IOSource            = 1027,

			/// <summary>Bytes (compressed) of the frame [frame index, ignored]</summary>
			FrameBytes          = 1028,

			/// <summary>Blocks in a given frame [frame index, ignored]</summary>
			FrameBlocks         = 1029,

			/// <summary>Point to tag (CAPETag *) [ignored, ignored]</summary>
			Tag                 = 1030,


			/// <summary>Current block location [ignored, ignored]</summary>
			DecompressCurrentBlock   = 2000,

			/// <summary>Current millisecond location [ignored, ignored]</summary>
			DecompressCurrentMs      = 2001,

			/// <summary>Total blocks in the decompressors range [ignored, ignored]</summary>
			DecompressTotalBlocks    = 2002,

			/// <summary>Length of the decompressors range in milliseconds [ignored, ignored]</summary>
			DecompressLengthMs       = 2003,

			/// <summary>Current bitrate [ignored, ignored]</summary>
			DecompressCurrentBitrate = 2004,

			/// <summary>Average bitrate (works with ranges) [ignored, ignored]</summary>
			DecompressAverageBitrate = 2005,

			/// <summary>Current frame</summary>
			DecompressCurrentFrame   = 2006,


			/// <summary>For internal use -- don't use (returns APE_FILE_INFO *) [ignored, ignored]</summary>
			InternalInfo             = 3000,
		}

		#endregion

		/// <summary>
		/// Initializes a new instance to an APE decompressor. (ASCII)
		/// </summary>
		/// <param name="filename">ASCII path to the APE file.</param>
		/// <param name="errorCode">Error code that is returned.</param>
		/// <returns>An handle to the APE decompressor instance.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr c_APEDecompress_Create(string filename, out int errorCode);

		/// <summary>
		/// Initializes a new instance to an APE decompressor. (Unicode)
		/// </summary>
		/// <param name="filename">Unicode path to the APE file.</param>
		/// <param name="errorCode">Error code that is returned.</param>
		/// <returns>An handle to the APE decompressor instance.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern IntPtr c_APEDecompress_CreateW(string filename, out int errorCode);

		/// <summary>
		/// Frees the decompressor represented by the given handle.
		/// </summary>
		/// <param name="handle">The handle to the decompressor instance to free.</param>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern void c_APEDecompress_Destroy(IntPtr handle);

		/// <summary>
		/// Gets decompressed samples and places them into the given buffer.
		/// </summary>
		/// <param name="handle">The handle to the current decompressor instance.</param>
		/// <param name="buffer">The buffer to place the decoded samples into.</param>
		/// <param name="numBlocks">The number of audio blocks desired.</param>
		/// <param name="blocksRetrieved">The number of blocks actually retrieved (could be less at end of file or on critical failure)</param>
		/// <returns>0 upon success, any other number for an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APEDecompress_GetData(IntPtr handle, sbyte[] buffer, int numBlocks, out int blocksRetrieved);

		/// <summary>
		/// Seeks to the given block offset.
		/// </summary>
		/// <param name="handle">The handle to the current decompressor</param>
		/// <param name="blockOffset">The block offset to seek to.</param>
		/// <returns>0 upon success, any other number for an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APEDecompress_Seek(IntPtr handle, int blockOffset);

		/// <summary>
		/// Get information about the APE file or the state of the decompressor.
		/// </summary>
		/// <param name="handle">Handle to the current decompressor.</param>
		/// <param name="field">The field we're querying (see APE_DECOMPRESS_FIELDS above for more info)</param>
		/// <param name="nParam1">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
		/// <param name="nParam2">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
		/// <returns>IntPtr to the retrieved info.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern IntPtr c_APEDecompress_GetInfo(IntPtr handle, DecompressInfo field, int nParam1, int nParam2);

		#endregion

		#region Compression

		/// <summary>
		/// Creates a compressor instance for encoding to APE.
		/// </summary>
		/// <param name="errorCode">Output error code.</param>
		/// <returns>A handle to the compressor instance.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr c_APECompress_Create(out int errorCode);

		/// <summary>
		/// Frees the compressor represented by the given handle.
		/// </summary>
		/// <param name="handle">The handle to the compressor instance to free.</param>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern void c_APECompress_Destroy(IntPtr handle);

		/// <summary>
		/// Starts encoding (ASCII filename)
		/// </summary>
		/// <param name="handle">Handle to the compresser instance.</param>
		/// <param name="outputFilename">Output filename of the APE file.</param>
		/// <param name="wfeInput">Format of the audio to encode (use FillWaveFormatEx() if necessary)</param>
		/// <param name="maxAudioBytes">
		/// The absolute maximum audio bytes that will be encoded... encoding fails with a
		/// ERROR_APE_COMPRESS_TOO_MUCH_DATA if you attempt to encode more than specified here
		/// (if unknown, use MAX_AUDIO_BYTES_UNKNOWN to allocate as much storage in the seek table as
		/// possible... limit is then 2 GB of data (~4 hours of CD music)... this wastes around
		/// 30kb, so only do it if completely necessary)
		/// </param>
		/// <param name="compressionLevel">
		/// The compression level for the APE file (fast - extra high)
		/// (note: extra-high is much slower for little gain)
		/// </param>
		/// <param name="headerData">
		///  A pointer to a buffer containing the WAV header (data before the data block in the WAV)
		/// (note: use NULL for on-the-fly encoding... see next parameter)
		/// </param>
		/// <param name="headerBytes">
		/// Number of bytes in the header data buffer (use CREATE_WAV_HEADER_ON_DECOMPRESSION and
		/// NULL for the pHeaderData and MAC will automatically create the appropriate WAV header
		/// on decompression)
		/// </param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		public static extern int c_APECompress_Start(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes);

		/// <summary>
		/// Starts encoding (Unicode filename)
		/// </summary>
		/// <param name="handle">Handle to the compresser instance.</param>
		/// <param name="outputFilename">Output filename of the APE file.</param>
		/// <param name="wfeInput">Format of the audio to encode (use FillWaveFormatEx() if necessary)</param>
		/// <param name="maxAudioBytes">
		/// The absolute maximum audio bytes that will be encoded... encoding fails with a
		/// ERROR_APE_COMPRESS_TOO_MUCH_DATA if you attempt to encode more than specified here
		/// (if unknown, use MAX_AUDIO_BYTES_UNKNOWN to allocate as much storage in the seek table as
		/// possible... limit is then 2 GB of data (~4 hours of CD music)... this wastes around
		/// 30kb, so only do it if completely necessary)
		/// </param>
		/// <param name="compressionLevel">
		/// The compression level for the APE file (fast - extra high)
		/// (note: extra-high is much slower for little gain)
		/// </param>
		/// <param name="headerData">
		///  A pointer to a buffer containing the WAV header (data before the data block in the WAV)
		/// (note: use NULL for on-the-fly encoding... see next parameter)
		/// </param>
		/// <param name="headerBytes">
		/// Number of bytes in the header data buffer (use CREATE_WAV_HEADER_ON_DECOMPRESSION and
		/// NULL for the pHeaderData and MAC will automatically create the appropriate WAV header
		/// on decompression)
		/// </param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		public static extern int c_APECompress_StartW(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes);

		/// <summary>
		/// Adds data to the encoder
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance.</param>
		/// <param name="data">Buffer containing the raw audio data.</param>
		/// <param name="numBytes">Number of bytes in the buffer.</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_AddData(IntPtr handle, byte[] data, int numBytes);

		/// <summary>
		/// Gets the number of bytes available in the buffer (helpful when locking)
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance.</param>
		/// <returns>The number of bytes available in the buffer.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_GetBufferBytesAvailable(IntPtr handle);

		/// <summary>
		/// Locks MAC's buffer so we can copy into it.
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance.</param>
		/// <param name="bytesAvailable">The number of bytes available in the buffer (DO NOT COPY MORE THAN THIS IN)</param>
		/// <returns>Pointer to the buffer (add at that location)</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr c_APECompress_LockBuffer(IntPtr handle, out int bytesAvailable);

		/// <summary>
		/// Releases the buffer.
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance</param>
		/// <param name="bytesAdded">The number of bytes copied into the buffer.</param>
		/// <param name="process">Whether or not MAC should process as much as possible of the buffer.</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_UnlockBuffer(IntPtr handle, int bytesAdded, [MarshalAs(UnmanagedType.Bool)]bool process);

		/// <summary>
		/// Ends encoding and finalizes the file.
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance</param>
		/// <param name="terminatingData">
		/// Pointer to a buffer containing the information to place at the end of the APE file
		/// (comprised of the WAV terminating data (data after the data block in the WAV) followed
		/// by any tag information).
		/// </param>
		/// <param name="terminatingBytes">Number of bytes in the terminating data buffer</param>
		/// <param name="wavTerminatingBytes">
		/// The number of bytes of the terminating data buffer that should be appended to a decoded
		/// WAV file (it's basically nTerminatingBytes - the bytes that make up the tag).
		/// </param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_Finish(IntPtr handle, IntPtr terminatingData, int terminatingBytes, int wavTerminatingBytes);

		/// <summary>
		/// Stops encoding and deletes the output file
		/// </summary>
		/// <param name="handle">Handle to the current compresser instance.</param>
		/// <returns>0 upon success, any other number indicates an error.</returns>
		[DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
		public static extern int c_APECompress_Kill(IntPtr handle);

		#endregion

		#endregion
	}
}
