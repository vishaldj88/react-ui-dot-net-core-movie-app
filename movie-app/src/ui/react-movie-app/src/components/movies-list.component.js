import React, { Component } from "react";
import { connect } from "react-redux";
import {
  retrieveMovies,
  findByName,
  deleteAllMovies,
} from "../actions/movies";
import { Link } from "react-router-dom";

class MoviesList extends Component {
  constructor(props) {
    super(props);
    //this.onChangeSearchFirstName = this.onChangeSearchFirstName.bind(this);
    this.refreshData = this.refreshData.bind(this);
    this.setActiveMovie = this.setActiveMovie.bind(this);
    this.findByName = this.findByName.bind(this);
    this.removeAllMovies = this.removeAllMovies.bind(this);

    this.state = {
      currentMovie: null,
      currentIndex: -1,
      searchName: "",
    };
  }

  componentDidMount() {
    this.props.retrieveMovies();
  }

  onChangeSearchName(e) {
    const searchName = e.target.value;

    this.setState({
      searchName: searchName,
    });
  }

  refreshData() {
    this.setState({
      currentMovie: null,
      currentIndex: -1,
    });
  }

  setActiveMovie(movie, index) {
    this.setState({
      currentMovie: movie,
      currentIndex: index,
    });
  }

  removeAllMovies() {
    this.props
      .deleteAllMovies()
      .then((response) => {
        console.log(response);
        this.refreshData();
      })
      .catch((e) => {
        console.log(e);
      });
  }

  findByName() {
    this.refreshData();

    this.props.findUsersByName(this.state.searchName);
  }

  render() {
    const { currentMovie, currentIndex } = this.state;
    const { movies } = this.props;

    return (

      <div className="container" >
        <div className="row">
          <div className="col-md-6">


            <h2>Movie Records</h2>
            <p>The records below consist's of movie release:</p>
            <table className="table table-bordered">
              <thead>
                <tr>
                  <th>#S.I</th>
                  <th>Name</th>
                  <th>Directory</th>
                  <th>Production</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {movies &&
                  movies.map((movie, index) => (

                    <tr
                      className={
                        (index === currentIndex ? "active" : "")
                      }
                      onClick={() => this.setActiveMovie(movie, index)}
                      key={index}
                    >
                      <td>{movie.id}
                      </td>
                      <td>{movie.name}
                      </td>

                      <td>{movie.director}
                      </td>
                      <td>{movie.release}
                      </td>
                      <td>
                        <a className="text-decoration-none"> View</a>
                      </td>
                    </tr>


                  ))}



              </tbody>
            </table>


            {/* <button
              className="m-3 btn btn-sm btn-danger"
              onClick={this.removeAllUsers}
            >
               Remove All 
          </button>*/}
          </div>


          <div className="col-md-1">

          </div>


          <div className="col-md-5">
            <br />
            <br />

            {currentMovie ? (





              <div className="form-group">
                <h4>Movie Detail</h4>
                <table class="table">
               
                  <tr>
                    <td className="label"> Name:{" "} </td>
                    <td>  {currentMovie.name}</td>
                  </tr>
                  <tr>
                    <td className="label"> Director:{" "}</td>
                    <td> {currentMovie.director}</td>
                  </tr>
                  <tr>
                    <td className="label"> Release:{" "}</td>
                    <td> {currentMovie.release}</td>
                  </tr>
                  <tr>
                    <td className="label"> Producer:{" "}</td>
                    <td> {currentMovie.producer}</td>
                  </tr>
                  <tr>
                    <td className="label"> In Demand:{" "}</td>
                    <td>{currentMovie.published ? "Yes" : "No"}</td>
                  </tr>
                </table>



                <Link
                  to={"/Movies/" + currentMovie.id}
                  className="btn badge-success"
                >
                  Edit
              </Link>
              </div>
            ) : (
                <div>
                  <br />
                  { <p>Please click on a View for Details...</p> }
                </div>
              )}
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    movies: state.movies,
  };
};

export default connect(mapStateToProps, {
  retrieveMovies,
  findByName,
  deleteAllMovies,
})(MoviesList);
