namespace MonkeysAudioSharp
{
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
        FileVersion = 1000,

        /// <summary>Compression level of the APE file [ignored, ignored]</summary>
        CompressionLevel = 1001,

        /// <summary>Format flags of the APE file [ignored, ignored]</summary>
        FormatFlags = 1002,

        /// <summary>Sample rate (Hz) [ignored, ignored]</summary>
        SampleRate = 1003,

        /// <summary>Bits per sample [ignored, ignored]</summary>
        BitsPerSample = 1004,

        /// <summary>Number of bytes per sample [ignored, ignored]</summary>
        BytesPerSample = 1005,

        /// <summary>Number of channels [ignored, ignored]</summary>
        Channels = 1006,

        /// <summary>Block alignment [ignored, ignored]</summary>
        BlockAlignment = 1007,

        /// <summary>Number of blocks in a frame (frames are used internally)  [ignored, ignored]</summary>
        BlocksPerFrame = 1008,

        /// <summary>Blocks in the final frame (frames are used internally) [ignored, ignored]</summary>
        FinalFrameBlocks = 1009,

        /// <summary>Total number frames (frames are used internally) [ignored, ignored]</summary>
        TotalFrames = 1010,

        /// <summary>Header bytes of the decompressed WAV [ignored, ignored]</summary>
        WavHeaderBytes = 1011,

        /// <summary>Terminating bytes of the decompressed WAV [ignored, ignored]</summary>
        WavTerminatingBytes = 1012,

        /// <summary>Data bytes of the decompressed WAV [ignored, ignored]</summary>
        WavDataBytes = 1013,

        /// <summary>Total bytes of the decompressed WAV [ignored, ignored]</summary>
        WavTotalBytes = 1014,

        /// <summary>Total bytes of the APE file [ignored, ignored]</summary>
        ApeTotalBytes = 1015,

        /// <summary>Total blocks of audio data [ignored, ignored]</summary>
        TotalBlocks = 1016,

        /// <summary>Length in ms (1 sec = 1000 ms) [ignored, ignored]</summary>
        LengthInMs = 1017,

        /// <summary>Average bitrate of the APE [ignored, ignored]</summary>
        AverageBitrate = 1018,

        /// <summary>Bitrate of specified APE frame [frame index, ignored]</summary>
        FrameBitrate = 1019,

        /// <summary>Bitrate of the decompressed WAV [ignored, ignored]</summary>
        DecompressedBitrate = 1020,

        /// <summary>Peak audio level (obsolete) (-1 is unknown) [ignored, ignored]</summary>
        PeakLevel = 1021,

        /// <summary>Bit offset [frame index, ignored]</summary>
        SeekBit = 1022,

        /// <summary>Byte offset [frame index, ignored]</summary>
        SeekByte = 1023,

        /// <summary>Error code [buffer *, max bytes]</summary>
        WavHeaderData = 1024,

        /// <summary>Error code [buffer *, max bytes]</summary>
        WavTerminatingData = 1025,

        /// <summary>Error code [waveformatex *, ignored]</summary>
        WaveFormatEx = 1026,

        /// <summary>I/O source (CIO *) [ignored, ignored]</summary>
        IOSource = 1027,

        /// <summary>Bytes (compressed) of the frame [frame index, ignored]</summary>
        FrameBytes = 1028,

        /// <summary>Blocks in a given frame [frame index, ignored]</summary>
        FrameBlocks = 1029,

        /// <summary>Point to tag (CAPETag *) [ignored, ignored]</summary>
        Tag = 1030,


        /// <summary>Current block location [ignored, ignored]</summary>
        DecompressCurrentBlock = 2000,

        /// <summary>Current millisecond location [ignored, ignored]</summary>
        DecompressCurrentMs = 2001,

        /// <summary>Total blocks in the decompressors range [ignored, ignored]</summary>
        DecompressTotalBlocks = 2002,

        /// <summary>Length of the decompressors range in milliseconds [ignored, ignored]</summary>
        DecompressLengthMs = 2003,

        /// <summary>Current bitrate [ignored, ignored]</summary>
        DecompressCurrentBitrate = 2004,

        /// <summary>Average bitrate (works with ranges) [ignored, ignored]</summary>
        DecompressAverageBitrate = 2005,

        /// <summary>Current frame</summary>
        DecompressCurrentFrame = 2006,


        /// <summary>For internal use -- don't use (returns APE_FILE_INFO *) [ignored, ignored]</summary>
        InternalInfo = 3000,
    }
}
