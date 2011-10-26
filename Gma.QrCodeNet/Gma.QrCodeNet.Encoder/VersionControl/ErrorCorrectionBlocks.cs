﻿namespace Gma.QrCodeNet.Encoding.VersionControl
{
	internal struct ErrorCorrectionBlocks
	{
		internal int NumErrorCorrectionCodewards { get; private set; }
		
		internal int NumBlocks { get; private set; }
		
		internal int ErrorCorrectionCodewordsPerBlock { get; private set;}
		
		private ErrorCorrectionBlock[] ecBlock;
		
		internal ErrorCorrectionBlocks(int numErrorCorrectionCodeWards, ErrorCorrectionBlock ecBlock)
			: this()
		{
			this.NumErrorCorrectionCodewards = numErrorCorrectionCodeWards;
			this.ecBlock = new ErrorCorrectionBlock[]{ecBlock};
			
			this.initialize();
		}
		
		internal ErrorCorrectionBlocks(int numErrorCorrectionCodeWards, ErrorCorrectionBlock ecBlock1, ErrorCorrectionBlock ecBlock2)
			: this()
		{
			this.NumErrorCorrectionCodewards = numErrorCorrectionCodeWards;
			this.ecBlock = new ErrorCorrectionBlock[]{ecBlock1, ecBlock2};
			
			this.initialize();
		}
		
		/// <summary>
		/// Get Error Correction Blocks
		/// </summary>
		internal ErrorCorrectionBlock[] GetECBlocks()
		{
			return ecBlock;
		}
		
		/// <summary>
		/// Initialize for NumBlocks and ErrorCorrectionCodewordsPerBlock
		/// </summary>
		private void initialize()
		{
			if(ecBlock == null)
				throw new System.ArgumentNullException("ErrorCorrectionBlocks array doesn't contain any value");
			
			NumBlocks = 0;
			int blockLength = ecBlock.Length;
			for(int i = 0; i < blockLength; i++)
			{
				NumBlocks += ecBlock[i].NumErrorCorrectionBlock;
			}
			
			
			ErrorCorrectionCodewordsPerBlock = NumErrorCorrectionCodewards / NumBlocks;
		}
	}
}
