using System;
using System.Collections.Generic;
using System.Linq;
using SiennaBadger.Data.Models;

namespace SiennaBadger.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<Word> Words(this string textContent)
        {
            var index = 0;
            List<Word> words = new List<Word>();

            Char[] content = textContent.ToCharArray();

            for (var i = 0; i < content.Length; i++)
            {
                if (!Char.IsLetter(content[i]) && content[i] != '-' && content[i] != '\'')
                {
                    var length = i - index;

                    if (length > 1)
                    {
                        var word = new String(content, index, length);
                        var wordMatch = words.SingleOrDefault(m =>
                            string.Equals(m.Text, word, StringComparison.CurrentCultureIgnoreCase));
                        if (wordMatch != null)
                        {
                            wordMatch.Count++;
                        }
                        else
                        {
                            words.Add(new Word()
                            {
                                Text = word,
                                Count = 1
                            });
                        }
                    }

                    index = i + 1;
                }
            }

            return words;
        }
    }
}
