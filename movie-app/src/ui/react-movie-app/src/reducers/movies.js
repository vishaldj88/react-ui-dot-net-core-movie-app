import {
  CREATE_MOVIE,
  RETRIEVE_MOVIES,
  UPDATE_MOVIE,
  DELETE_MOVIE,
  DELETE_ALL_MOVIES,
} from "../actions/types";

const initialState = [];

function movieReducer(movies = initialState, action) {
  const { type, payload } = action;

  switch (type) {
    case CREATE_MOVIE:
      return [...movies, payload];

    case RETRIEVE_MOVIES:
      return payload;

    case UPDATE_MOVIE:
      return movies.map((movie) => {
        if (movie.id === payload.id) {
          return {
            ...movie,
            ...payload,
          };
        } else {
          return movie;
        }
      });

    case DELETE_MOVIE:
      return movies.filter(({ id }) => id !== payload.id);

    case DELETE_ALL_MOVIES:
      return [];

    default:
      return movies;
  }
};

export default movieReducer;