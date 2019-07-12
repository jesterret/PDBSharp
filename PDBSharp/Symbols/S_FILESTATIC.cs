#region License
/*
 * Copyright (C) 2019 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using Smx.PDBSharp.Leaves;
using Smx.PDBSharp.Symbols.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smx.PDBSharp.Symbols
{
	public class FileStaticSym
	{
		public LeafBase Type { get; set; }
		public UInt32 ModuleFilenameOffset { get; set; }
		public CV_LVARFLAGS Flags { get; set; }
		public string Name { get; set; }
	}

	public class S_FILESTATIC : ISymbol
	{
		public readonly ILeafContainer Type;
		public readonly UInt32 ModuleFilenameOffset;
		public readonly CV_LVARFLAGS Flags;
		public readonly string Name;

		public S_FILESTATIC(PDBFile pdb, Stream stream) {
			var r = new SymbolDataReader(pdb, stream);
			Type = r.ReadIndexedTypeLazy();
			ModuleFilenameOffset = r.ReadUInt32();
			Flags = r.ReadFlagsEnum<CV_LVARFLAGS>();
			Name = r.ReadSymbolString();
		}

		public S_FILESTATIC(FileStaticSym data) {
			Type = data.Type;
			ModuleFilenameOffset = data.ModuleFilenameOffset;
			Flags = data.Flags;
			Name = data.Name;
		}

		public void Write(PDBFile pdb, Stream stream) {
			var w = new SymbolDataWriter(pdb, stream, SymbolType.S_FILESTATIC);
			w.WriteIndexedType(Type);
			w.WriteUInt32(ModuleFilenameOffset);
			w.WriteEnum<CV_LVARFLAGS>(Flags);
			w.WriteSymbolString(Name);

			w.WriteSymbolHeader();
		}
	}
}
