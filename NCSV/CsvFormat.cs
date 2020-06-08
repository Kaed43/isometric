using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCSV
{
    public static class CsvFormat
    {
        public static IEnumerable<string> DecodeRow(string data)
        {
            // the field content
            StringBuilder fieldSoFar = new StringBuilder();
            // TRUE if the field is empty or only whitespace so far
            bool noDataYet = true;
            // TRUE if the reader is currently in quotes
            bool isInQuotes = false;
            // TRUE if the reader has exited a quoted field
            bool closedQuoteData = false;

            // We finish a field on a comma, so to send the final field, I'm just gonna slap another comma on there
            // Not ideal, but it should work.
            data += ',';

            for(var i = 0; i < data.Length; i++)
            {
                char c = data[i];
                switch (c)
                {
                    case ',':
                        if(isInQuotes)
                        {
                            fieldSoFar.Append(',');
                        }
                        else
                        {
                            // Ok, the field is over
                            if(closedQuoteData)
                            {
                                // If we've closed a quote, we've already sent the field, just reset
                                closedQuoteData = false;
                            }
                            else
                            {
                                yield return fieldSoFar.ToString();
                                fieldSoFar.Clear();
                                noDataYet = true;
                                isInQuotes = false;
                                closedQuoteData = false;
                            }
                        }
                        break;
                    case '"':
                        if (closedQuoteData) continue;
                        if (isInQuotes)
                        {
                            // Is this an escaped quote?
                            if(i < data.Length - 1 && data[i+1] == '"')
                            {
                                fieldSoFar.Append('"');
                                i++;
                            }
                            // Nvm, this is not an escaped quote, this is an end quote
                            else
                            {
                                yield return fieldSoFar.ToString();
                                fieldSoFar.Clear();
                                isInQuotes = false;
                                closedQuoteData = true;
                                noDataYet = true;
                            }
                        }
                        // Ok, this is probably an open quote, but it might be a quote-mid-data
                        else
                        {
                            // No data yet? This is an open quote then.
                            if (noDataYet)
                            {
                                fieldSoFar.Clear();
                                isInQuotes = true;
                            }
                            // Nvm, this is a quote-mid-data
                            else
                            {
                                fieldSoFar.Append('"');
                            }
                        }
                        break;
                    default:
                        // If we've already read the end-quote, ignore any remaining "data" until the comma
                        if (closedQuoteData) continue;

                        // Otherwise, append the character to the field
                        fieldSoFar.Append(c);
                        // And if it's not whitespace, we have data
                        if (!char.IsWhiteSpace(c))
                            noDataYet = false;
                        break;
                }
            }
        }

        public static string EncodeRow(IEnumerable<string> data)
        {
            return string.Join(",", data.Select(dat =>
            {
                if (dat.Contains(',') || dat.Contains('"'))
                    return dat.Replace("\"", "\"\"");
                return dat;
            }));
        }
    }
}
