﻿using AsciiConsoleUi;
using AsciiUml.Geo;

namespace AsciiUml.Commands {
	internal class CreateDatabase : ICommand {
		public readonly Coord pos;

		public CreateDatabase(Coord pos) {
			this.pos = pos;
		}

		public State Execute(State state) {
			var db = new Database(pos);
			state.Model.Objects.Add(db);
			state.SelectedId = db.Id;
			state.SelectedIndexInModel = state.Model.Objects.Count - 1;
			return state;
		}
	}
}