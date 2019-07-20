#region License
/*
 * Copyright (C) 2019 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Smx.PDBSharp
{
	public class NameTableReader
	{
		public readonly byte[] StringTableData;

		private readonly Dictionary<uint, uint> Offset_Index;
		private readonly Dictionary<uint, uint> Index_Offset;

		private readonly ReaderBase rdr;

		private readonly Dictionary<string, uint> String_Index = new Dictionary<string, uint>();
		private readonly Dictionary<uint, string> Index_String = new Dictionary<uint, string>();

		public string GetString(uint index) {
			if (!Index_Offset.ContainsKey(index)) {
				return null;
			}

			if (Index_String.TryGetValue(index, out string cachedString)) {
				return cachedString;
			}

			uint offset = Index_Offset[index];
			rdr.BaseStream.Position = offset;
			string str = rdr.ReadCString();

			Index_String.Add(index, str);
			return str;
		}

		public bool GetIndex(string str, out uint index) {
			if (String_Index.TryGetValue(str, out uint cachedIndex)) {
				index = cachedIndex;
				return true;
			}

			uint? _index = Offset_Index
				.Where(p => GetString(p.Value) == str)
				.Select(p => p.Value)
				.Cast<uint?>()
				.FirstOrDefault();

			if (_index == null) {
				index = 0;
				return false;
			}

			index = _index.Value;
			String_Index.Add(str, index);

			return true;
		}

		public NameTableReader(ReaderBase r) {
			StringTableData = Deserializers.ReadBuffer(r);
			rdr = new ReaderBase(new MemoryStream(StringTableData));

			Offset_Index = Deserializers.ReadMap<uint, uint>(r);

			uint maxNameIndices = r.ReadUInt32();

			Index_Offset = Offset_Index.ToDictionary(x => x.Value, x => x.Key);
		}
	}
}
