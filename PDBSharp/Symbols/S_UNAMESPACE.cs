#region License
/*
 * Copyright (C) 2019 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
﻿using Smx.PDBSharp.Symbols.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smx.PDBSharp.Symbols
{
	public class S_UNAMESPACE : ISymbol
	{
		public readonly string NamespaceName;

		public S_UNAMESPACE(Context ctx, IModule mod, Stream stream) {
			var r = new SymbolDataReader(ctx, stream);
			NamespaceName = r.ReadSymbolString();
		}

		public S_UNAMESPACE(string namespaceName) {
			this.NamespaceName = namespaceName;
		}

		public void Write(PDBFile pdb, Stream stream) {
			var w = new SymbolDataWriter(pdb, stream, SymbolType.S_UNAMESPACE);
			w.WriteSymbolString(NamespaceName);

			w.WriteSymbolHeader();
		}
	}
}
