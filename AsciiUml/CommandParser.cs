﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanguageExt;
using LanguageExt.SomeHelp;

namespace AsciiUml {
	public static class Ext {
		public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> filter,
			Action<int> foundActionWithPosition) {
			int pos = 0;

			var res = collection.FirstOrDefault(
				x => {
					bool filterRes = filter(x);
					if (filterRes) {
						foundActionWithPosition(pos);
					}
					pos++;
					return filterRes;
				});

			return res;
		}

		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> coll, int skip) {
			return coll.Reverse().Skip(skip).Reverse();
		}

		public static void Each<T>(this IEnumerable<T> coll, Action<T> code) {
			foreach (var c in coll) {
				code(c);
			}
		}

		public static void Each<T>(this IEnumerable<T> coll, Action<T, int> code) {
			int i = 0;
			foreach (var c in coll) {
				code(c, i++);
			}
		}
	}

	public class Range<T> {
		public T Min, Max;

		public Range(T min, T max) {
			Min = min;
			Max = max;
		}

		public override string ToString() {
			return $"{Min} - {Max}";
		}
	}

	public static class CommandParser {
		public static Option<int> ReadInt(Range<int> range, string text) {
			Console.Write(text);
			var input = TryReadLineWithCancel();
			return input.Match(x => {
				int result;
				if (!int.TryParse(x, out result))
					return ReadInt(range, "\nNot a number. Try again: ");

				if (result < range.Min || result > range.Max)
					return ReadInt(range, $"\nNot in range {range}: ");

				return result.ToSome();
			}, () => Option<int>.None);
		}

		public static Option<string> TryReadLineWithCancel() {
			var sb = new StringBuilder();

			do {
				var key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					return sb.ToString();
				if (key.Key == ConsoleKey.Escape)
					return null;
				if (key.Key == ConsoleKey.Backspace) {
					if (sb.Length > 0) {
						sb.Remove(sb.Length - 1, 1);
						Console.Write("\b \b");
					}
				}
				else {
					Console.Write(key.KeyChar);
					sb.Append(key.KeyChar);
				}
			} while (true);
		}

		public static void Parse(string s) {
			if (s == "print") {
			}
		}
	}
}