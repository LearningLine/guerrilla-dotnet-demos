using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payments
{
    public class SwipeCardDecoder
    {
        public CardDetails Decode(string rawData)
        {
            if (rawData[1] != 'B')
            {
                throw new CardDecodingException("Incorrect card type");
            }
            CardDetails returnDetails = null;
            CardDetails track1Details = GetTrack1Details(rawData);
            returnDetails = track1Details;

            CardDetails track2Details = GetTrack2Details(rawData);

            if (track2Details != null && track1Details.Number != track2Details.Number)
            {
                track2Details.Name = track1Details.Name;
                returnDetails = track2Details;
            }

            return returnDetails;
        }

        private static CardDetails GetTrack2Details(string rawData)
        {
            int track2Separator = rawData.LastIndexOf(';');

            if (track2Separator == -1)
            {
                return null;
            }

            int separator = rawData.IndexOf('=', track2Separator + 1);

            var details = new CardDetails(rawData.Substring(track2Separator + 1, separator - track2Separator - 1),
                                          "",
                                          rawData.Substring(separator + 3, 2),
                                          rawData.Substring(separator + 1, 2));

            return details;
        }

        private static CardDetails GetTrack1Details(string rawData)
        {
            var dataChars = rawData.Substring(2).ToList();
            char separatorChar = dataChars.Find(c => !char.IsDigit(c) && !char.IsLetter(c));

            int firstSeparatorIndex = rawData.IndexOf(separatorChar);
            int secondSeparatorIndex = rawData.IndexOf(separatorChar, firstSeparatorIndex + 1);

            return new CardDetails(rawData.Substring(2, firstSeparatorIndex - 2),
                                   rawData.Substring(firstSeparatorIndex + 1,
                                                     secondSeparatorIndex - firstSeparatorIndex - 1).Trim(),
                                   rawData.Substring(secondSeparatorIndex + 3, 2),
                                   rawData.Substring(secondSeparatorIndex + 1, 2));
        }
    }
}
