using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Word2NumberConverter.Models
{
    /// <summary>
    /// Common interface which will be used by different Number system classes.
    /// </summary>
    public interface IWord2NumberConverter
    {
        /// <summary>
        /// Common method to be implemented for calculating words to number.
        /// </summary>
        /// <param name="numberInWords">String imput containing the word description of number</param>
        /// <returns>Number calculated from words.</returns>
        public BigInteger ConvertWordsToNumber(string numberInWords);
    }
}
