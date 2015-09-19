using System;
using System.Text;
using MonkeysAudioSharp;
using NAudio.Wave;

namespace Test
{
	/// <summary>
	/// Test program for the wrapper.
	/// </summary>
	internal sealed class Program
	{
		private static void Main(string[] args)
		{
			// Initialization
			Console.OutputEncoding = Encoding.UTF8;
			var ape = new ApeReader(args[0]);
			DisplayInformation(ape.Handle);

			// Set up the player.
			IWavePlayer player = new WaveOut();
			player.Init(ape);
			player.Play();

			// Listen for key presses to pause/play audio.
			var isPlaying = true;
			do
			{
				var keyInfo = Console.ReadKey();
				if (keyInfo.Key == ConsoleKey.Spacebar)
				{
					if (isPlaying)
					{
						player.Pause();
						isPlaying = false;
					}
					else
					{
						player.Play();
						isPlaying = true;
					}
				}

				// If we hit the end of the stream in terms of actual audio data.
				if (isPlaying && ape.Position == ape.Length)
				{
					player.Stop();
				}

			} while (player.PlaybackState != PlaybackState.Stopped);
		}

		private static void DisplayInformation(IntPtr handle)
		{
			Console.WriteLine("Number of channels     : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.Channels));
			Console.WriteLine("Sample rate (Hz)       : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.SampleRate)); ;
			Console.WriteLine("Compression Level      : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.CompressionLevel));
			Console.WriteLine("Average Bitrate        : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.AverageBitrate));
			Console.WriteLine("Bits per sample        : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.BitsPerSample));
			Console.WriteLine("Block Alignment        : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.BlockAlignment));
			Console.WriteLine("Blocks per frame       : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.BlocksPerFrame));
			Console.WriteLine("Total Frames           : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.TotalFrames));
			Console.WriteLine("Total Blocks           : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.TotalBlocks));
			Console.WriteLine("Total size (compressed): " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.ApeTotalBytes));
			Console.WriteLine("Length in milliseconds : " + ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.LengthInMs));
			Console.WriteLine("Length in seconds      : " + (ApeNative.Decompress_GetInfoInt(handle, DecompressInfo.LengthInMs) / 1000));
		}
	}
}
