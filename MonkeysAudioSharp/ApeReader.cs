using System;
using NAudio.Wave;

namespace MonkeysAudioSharp
{
	/// <summary>
	/// <see cref="WaveStream"/> for reading Monkey's Audio files
	/// </summary>
	public sealed class ApeReader : WaveStream
	{
	    /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filename">Path to the APE file to read.</param>
		public ApeReader(string filename)
		{
			// Attempt to get a decoding instance.
			int ret;
			Handle = ApeNative.c_APEDecompress_Create(filename, out ret);
			if (ret != 0)
				throw new ArgumentException("Unable to create a decoder.");

			// Initialize the WaveFormat
			var sampleRate    = ApeNative.Decompress_GetInfoInt(Handle, DecompressInfo.SampleRate);
			var bitsPerSample = ApeNative.Decompress_GetInfoInt(Handle, DecompressInfo.BitsPerSample);
			var channels      = ApeNative.Decompress_GetInfoInt(Handle, DecompressInfo.Channels);
			WaveFormat =  new WaveFormat(sampleRate, bitsPerSample, channels);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var tempBuf = new sbyte[count];

			// Decode samples into tempBuf.
			int retrievedBlocks;
			ApeNative.c_APEDecompress_GetData(Handle, tempBuf, count/BlockAlign, out retrievedBlocks);

			// Shift them over to the actual sample buffer for NAudio.
			Buffer.BlockCopy(tempBuf,0, buffer, 0, tempBuf.Length);
			return count;
		}

		public override WaveFormat WaveFormat { get; }

	    public override long Length => ApeNative.Decompress_GetInfoInt(Handle, DecompressInfo.TotalBlocks) * BlockAlign;

	    public override long Position
		{
			get
			{
				var blockPos = ApeNative.Decompress_GetInfoInt(Handle, DecompressInfo.DecompressCurrentBlock);

				// Convert the block position into a byte offset.
				return (blockPos * BlockAlign);
			}

			set
			{
				// Convert to a block position
				var blockPos = (value / BlockAlign);

				ApeNative.c_APEDecompress_Seek(Handle, (int) blockPos);
			}
		}

		protected override void Dispose(bool disposing)
		{
			ApeNative.c_APEDecompress_Destroy(Handle);
			base.Dispose(disposing);
		}

		/// <summary>
		/// Underlying decoding handle this reader uses.
		/// </summary>
		public IntPtr Handle
		{
			get;
			private set;
		}
	}
}
