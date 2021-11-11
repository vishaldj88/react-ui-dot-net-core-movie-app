import {
  CREATE_MOVIE,
  RETRIEVE_MOVIES,
  UPDATE_MOVIE,
  DELETE_MOVIE,
  DELETE_ALL_MOVIES
} from "./types";

import MovieDataService from "../services/movie.service";

export const createMovie = (name, director,producer,release,hit) => async (dispatch) => {
  try {
    const res = await MovieDataService.create({ name, director,producer,release,hit });

    dispatch({
      type: CREATE_MOVIE,
      payload: res.data,
    });

    return Promise.resolve(res.data);
  } catch (err) {
    return Promise.reject(err);
  }
};

export const retrieveMovies = () => async (dispatch) => {
  try {
    const res = await MovieDataService.getAll();
    console.log(res);
    dispatch({
      type: RETRIEVE_MOVIES,
      payload: res.data,
    });
  } catch (err) {
    console.log(err);
  }
};

export const updateMovie = (id, data) => async (dispatch) => {
  try {
    const res = await MovieDataService.update(id, data);

    dispatch({
      type: UPDATE_MOVIE,
      payload: data,
    });

    return Promise.resolve(res.data);
  } catch (err) {
    return Promise.reject(err);
  }
};

export const deleteMovie = (id) => async (dispatch) => {
  try {
    await MovieDataService.delete(id);

    dispatch({
      type: DELETE_MOVIE,
      payload: { id },
    });
  } catch (err) {
    console.log(err);
  }
};

export const deleteAllMovies = () => async (dispatch) => {
  try {
    const res = await MovieDataService.deleteAll();

    dispatch({
      type: DELETE_ALL_MOVIES,
      payload: res.data,
    });

    return Promise.resolve(res.data);
  } catch (err) {
    return Promise.reject(err);
  }
};

export const findByName = (name) => async (dispatch) => {
  try {
    const res = await MovieDataService.findByName(name);

    dispatch({
      type: RETRIEVE_MOVIES,
      payload: res.data,
    });
  } catch (err) {
    console.log(err);
  }
};