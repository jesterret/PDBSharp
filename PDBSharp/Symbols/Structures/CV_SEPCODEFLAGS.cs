#region License
/*
 * Copyright (C) 2018 Stefano Moioli <smxdev4@gmail.com>
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
#endregion
using System;

namespace Smx.PDBSharp.Symbols.Structures
{
	[Flags]
	public enum CV_SEPCODEFLAGS : uint
	{
		IsLexicalScope = 1 << 0,
		ReturnsToParent = 1 << 1
	}
}