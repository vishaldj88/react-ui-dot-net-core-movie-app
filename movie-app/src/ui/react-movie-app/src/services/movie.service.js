import http from "../http-common";

class MovieDataService {
  getAll() {
    return http.get("/movies?Size=25");
  }

  get(id) {
    return http.get(`/movie/${id}`);
  }

  create(data) {
    return http.post("/movie", data);
  }

  update(id, data) {
    return http.put(`/movie/${id}`, data);
  }

  delete(id) {
    return http.delete(`/movie/${id}`);
  }

  deleteAll() {
    return http.delete(`/movie`);
  }

}

export default new MovieDataService();