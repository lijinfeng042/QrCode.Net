﻿using System;

namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    internal class AlphanumericEncoder : EncoderBase
    {
        public AlphanumericEncoder(int version) 
            : base(version)
        {
        }

        internal override Mode Mode
        {
            get { return Mode.Alphanumeric; }
        }

        internal override BitList GetDataBits(string content)
        {
        	BitList dataBits = new BitList();
        	int contentLength = content.Length;
            for (int i = 0; i < contentLength; i += 2)
            {
                int groupLength = Math.Min(2, contentLength-i);
                int value = GetAlphaNumValue(content, i, groupLength);
                int bitCount = GetBitCountByGroupLength(groupLength);
                dataBits.Add(value, bitCount);
            }
			return dataBits;
        }
        
    	
    	/// <summary>
    	/// Constant from Chapter 8.4.3 Alphanumeric Mode. P.21
    	/// </summary>
    	private const int s_MultiplyFirstChar = 45;
    	
        private static int GetAlphaNumValue(string content, int startIndex, int length)
        {
        	int value = 0;
        	int iMultiplyValue = 1;
        	for (int i = 0 ; i < length; i++)
        	{
        		int positionFromEnd = startIndex + length - i - 1;
        	    int code = AlphanumericTable.ConvertAlphaNumChar(content[positionFromEnd]);
        	    value += code * iMultiplyValue;
        		iMultiplyValue *= s_MultiplyFirstChar;
        	}
        	return value;
        }
        

        protected override int GetBitCountInCharCountIndicator()
        {
            return CharCountIndicatorTable.GetBitCountInCharCountIndicator(Mode.Alphanumeric, base.Version);
        }
        
        /// <summary>
        /// BitCount from chapter 8.4.3. P22
        /// </summary>
        protected int GetBitCountByGroupLength(int groupLength)
        {
            switch (groupLength)
            {
                case 0:
                    return 0;
                case 1:
                    return 6;
                case 2:
                    return 11;
                default:
                    throw new InvalidOperationException(string.Format("Unexpected group length {0}", groupLength));
            }
        }
    }
}