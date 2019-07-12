#region License
/*
 * Copyright (C) 2019 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using Smx.PDBSharp.Symbols.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Smx.PDBSharp.Symbols
{
	public class S_COMPILE : ISymbol
	{
		public readonly CompileSymFlags Flags;
		public readonly byte Machine;
		public readonly string VersionString;

		public S_COMPILE(PDBFile pdb, Stream stream) {
			var r = new SymbolDataReader(pdb, stream);
			Machine = r.ReadByte();
			uint flags = (uint)(r.ReadByte() | (r.ReadByte() << 8) | (r.ReadByte() << 16));
			Flags = new CompileSymFlags(flags);
			VersionString = r.ReadSymbolString();
		}

		public void Write(PDBFile pdb, Stream stream) {
			throw new NotImplementedException();
		}
	}
}
