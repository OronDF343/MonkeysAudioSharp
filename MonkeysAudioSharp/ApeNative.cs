using System;

namespace MonkeysAudioSharp
{
    /// <summary>
	/// Interface for P/Invoke methods (portable)
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

        #region Misc

        /// <summary>
        /// Gets the codec version number.
        /// </summary>
        /// <returns>The version number of the codec</returns>
        public static int GetVersionNumber()
        {
            return Environment.Is64BitProcess ? ApeNative64.GetVersionNumber() : ApeNative32.GetVersionNumber();
        }

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
        public static int TagFileSimple(string filename, string artist, string album, string title, string comment, string genre, string year, string track, bool clearFirst, bool useOldId3)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.TagFileSimple(filename, artist, album, title, comment, genre, year, track,
                                                   clearFirst, useOldId3)
                       : ApeNative32.TagFileSimple(filename, artist, album, title, comment, genre, year, track,
                                                   clearFirst, useOldId3);
        }

        /// <summary>
        /// Gets the ID3 tag within the given APE file.
        /// </summary>
        /// <param name="filename">Path to the APE file.</param>
        /// <param name="id3Tag">The retrieved ID3 tag</param>
        /// <returns>0 upon success, any other number indicates an error.</returns>
        public static int GetID3Tag(string filename, IntPtr id3Tag)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.GetID3Tag(filename, id3Tag)
                       : ApeNative32.GetID3Tag(filename, id3Tag);
        }

        /// <summary>
        /// Removes the ID3 tag from the given APE file if it exists.
        /// </summary>
        /// <param name="filename">Path to the APE file.</param>
        /// <returns>0 upon success, any other number indicates an error.</returns>
        public static int RemoveTag(string filename)
        {
            return Environment.Is64BitProcess ? ApeNative64.RemoveTag(filename) : ApeNative32.RemoveTag(filename);
        }

        #endregion

        #region Decompression

        /// <summary>
        /// Initializes a new instance to an APE decompressor. (ASCII)
        /// </summary>
        /// <param name="filename">ASCII path to the APE file.</param>
        /// <param name="errorCode">Error code that is returned.</param>
        /// <returns>An handle to the APE decompressor instance.</returns>
        public static IntPtr c_APEDecompress_Create(string filename, out int errorCode)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APEDecompress_Create(filename, out errorCode)
                       : ApeNative32.c_APEDecompress_Create(filename, out errorCode);
        }

        /// <summary>
        /// Initializes a new instance to an APE decompressor. (Unicode)
        /// </summary>
        /// <param name="filename">Unicode path to the APE file.</param>
        /// <param name="errorCode">Error code that is returned.</param>
        /// <returns>An handle to the APE decompressor instance.</returns>
        public static IntPtr c_APEDecompress_CreateW(string filename, out int errorCode)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APEDecompress_CreateW(filename, out errorCode)
                       : ApeNative32.c_APEDecompress_CreateW(filename, out errorCode);
        }

        /// <summary>
        /// Frees the decompressor represented by the given handle.
        /// </summary>
        /// <param name="handle">The handle to the decompressor instance to free.</param>
        public static void c_APEDecompress_Destroy(IntPtr handle)
        {
            if (Environment.Is64BitProcess) ApeNative64.c_APEDecompress_Destroy(handle);
            else ApeNative32.c_APEDecompress_Destroy(handle);
        }

        /// <summary>
        /// Gets decompressed samples and places them into the given buffer.
        /// </summary>
        /// <param name="handle">The handle to the current decompressor instance.</param>
        /// <param name="buffer">The buffer to place the decoded samples into.</param>
        /// <param name="numBlocks">The number of audio blocks desired.</param>
        /// <param name="blocksRetrieved">The number of blocks actually retrieved (could be less at end of file or on critical failure)</param>
        /// <returns>0 upon success, any other number for an error.</returns>
        public static int c_APEDecompress_GetData(IntPtr handle, sbyte[] buffer, int numBlocks, out int blocksRetrieved)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APEDecompress_GetData(handle, buffer, numBlocks, out blocksRetrieved)
                       : ApeNative32.c_APEDecompress_GetData(handle, buffer, numBlocks, out blocksRetrieved);
        }

        /// <summary>
        /// Seeks to the given block offset.
        /// </summary>
        /// <param name="handle">The handle to the current decompressor</param>
        /// <param name="blockOffset">The block offset to seek to.</param>
        /// <returns>0 upon success, any other number for an error.</returns>
        public static int c_APEDecompress_Seek(IntPtr handle, int blockOffset)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APEDecompress_Seek(handle, blockOffset)
                       : ApeNative32.c_APEDecompress_Seek(handle, blockOffset);
        }

        /// <summary>
        /// Get information about the APE file or the state of the decompressor.
        /// </summary>
        /// <param name="handle">Handle to the current decompressor.</param>
        /// <param name="field">The field we're querying (see APE_DECOMPRESS_FIELDS above for more info)</param>
        /// <param name="nParam1">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
        /// <param name="nParam2">Generic parameter - usage is listed in <see cref="DecompressInfo"/></param>
        /// <returns>IntPtr to the retrieved info.</returns>
        public static IntPtr c_APEDecompress_GetInfo(IntPtr handle, DecompressInfo field, int nParam1, int nParam2)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APEDecompress_GetInfo(handle, field, nParam1, nParam2)
                       : ApeNative32.c_APEDecompress_GetInfo(handle, field, nParam1, nParam2);
        }

        #endregion

        #region Compression

        /// <summary>
        /// Creates a compressor instance for encoding to APE.
        /// </summary>
        /// <param name="errorCode">Output error code.</param>
        /// <returns>A handle to the compressor instance.</returns>
        public static IntPtr c_APECompress_Create(out int errorCode)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_Create(out errorCode)
                       : ApeNative32.c_APECompress_Create(out errorCode);
        }

        /// <summary>
        /// Frees the compressor represented by the given handle.
        /// </summary>
        /// <param name="handle">The handle to the compressor instance to free.</param>
        public static void c_APECompress_Destroy(IntPtr handle)
        {
            if (Environment.Is64BitProcess) ApeNative64.c_APECompress_Destroy(handle);
            else ApeNative32.c_APECompress_Destroy(handle);
        }

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
        public static int c_APECompress_Start(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_Start(handle, outputFilename, wfeInput, maxAudioBytes,
                                                         compressionLevel, headerData, headerBytes)
                       : ApeNative32.c_APECompress_Start(handle, outputFilename, wfeInput, maxAudioBytes,
                                                         compressionLevel, headerData, headerBytes);
        }

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
        public static int c_APECompress_StartW(IntPtr handle, string outputFilename, IntPtr wfeInput, int maxAudioBytes, int compressionLevel, IntPtr headerData, int headerBytes)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_StartW(handle, outputFilename, wfeInput, maxAudioBytes,
                                                          compressionLevel, headerData, headerBytes)
                       : ApeNative32.c_APECompress_StartW(handle, outputFilename, wfeInput, maxAudioBytes,
                                                          compressionLevel, headerData, headerBytes);
        }

        /// <summary>
        /// Adds data to the encoder
        /// </summary>
        /// <param name="handle">Handle to the current compresser instance.</param>
        /// <param name="data">Buffer containing the raw audio data.</param>
        /// <param name="numBytes">Number of bytes in the buffer.</param>
        /// <returns>0 upon success, any other number indicates an error.</returns>
        public static int c_APECompress_AddData(IntPtr handle, byte[] data, int numBytes)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_AddData(handle, data, numBytes)
                       : ApeNative32.c_APECompress_AddData(handle, data, numBytes);
        }

        /// <summary>
        /// Gets the number of bytes available in the buffer (helpful when locking)
        /// </summary>
        /// <param name="handle">Handle to the current compresser instance.</param>
        /// <returns>The number of bytes available in the buffer.</returns>
        public static int c_APECompress_GetBufferBytesAvailable(IntPtr handle)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_GetBufferBytesAvailable(handle)
                       : ApeNative32.c_APECompress_GetBufferBytesAvailable(handle);
        }

        /// <summary>
        /// Locks MAC's buffer so we can copy into it.
        /// </summary>
        /// <param name="handle">Handle to the current compresser instance.</param>
        /// <param name="bytesAvailable">The number of bytes available in the buffer (DO NOT COPY MORE THAN THIS IN)</param>
        /// <returns>Pointer to the buffer (add at that location)</returns>
        public static IntPtr c_APECompress_LockBuffer(IntPtr handle, out int bytesAvailable)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_LockBuffer(handle, out bytesAvailable)
                       : ApeNative32.c_APECompress_LockBuffer(handle, out bytesAvailable);
        }

        /// <summary>
        /// Releases the buffer.
        /// </summary>
        /// <param name="handle">Handle to the current compresser instance</param>
        /// <param name="bytesAdded">The number of bytes copied into the buffer.</param>
        /// <param name="process">Whether or not MAC should process as much as possible of the buffer.</param>
        /// <returns>0 upon success, any other number indicates an error.</returns>
        public static int c_APECompress_UnlockBuffer(IntPtr handle, int bytesAdded, bool process)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_UnlockBuffer(handle, bytesAdded, process)
                       : ApeNative32.c_APECompress_UnlockBuffer(handle, bytesAdded, process);
        }

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
        public static int c_APECompress_Finish(IntPtr handle, IntPtr terminatingData, int terminatingBytes, int wavTerminatingBytes)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_Finish(handle, terminatingData, terminatingBytes, wavTerminatingBytes)
                       : ApeNative32.c_APECompress_Finish(handle, terminatingData, terminatingBytes, wavTerminatingBytes);
        }

        /// <summary>
        /// Stops encoding and deletes the output file
        /// </summary>
        /// <param name="handle">Handle to the current compresser instance.</param>
        /// <returns>0 upon success, any other number indicates an error.</returns>
        public static int c_APECompress_Kill(IntPtr handle)
        {
            return Environment.Is64BitProcess
                       ? ApeNative64.c_APECompress_Kill(handle)
                       : ApeNative32.c_APECompress_Kill(handle);
        }

        #endregion
    }
}
