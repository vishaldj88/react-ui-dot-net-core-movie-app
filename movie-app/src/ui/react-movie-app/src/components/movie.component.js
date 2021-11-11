import React, { Component } from "react";
import { connect } from "react-redux";
import { updateMovie, deleteMovie } from "../actions/movies";
import MovieDataService from "../services/movie.service";

class Movie extends Component {
  constructor(props) {
    super(props);
    this.onChangeName = this.onChangeName.bind(this);
    this.onChangeDirector = this.onChangeDirector.bind(this);
    this.getMovie = this.getMovie.bind(this);
    this.updateHit = this.updateStatus.bind(this);
    this.updateContent = this.updateContent.bind(this);
    this.removeMovie = this.removeMovie.bind(this);

    this.state = {
      currentMovie: {
        id: null,
        name: "",
        director: "",
        hit: false,
        release: "",
        producer: "",
        submitted: false,
        published: false,
      },
      message: "",
    };
  }

  componentDidMount() {
    this.getMovie(this.props.match.params.id);
  }

  onChangeName(e) {
    const name = e.target.value;

    this.setState(function (prevState) {
      return {
        currentMovie: {
          ...prevState.currentMovie,
          name: name,
        },
      };
    });
  }

  onChangeDirector(e) {
    const director = e.target.value;

    this.setState((prevState) => ({
      currentMovie: {
        ...prevState.currentMovie,
        director: director,
      },
    }));
  }

  getMovie(id) {
    MovieDataService.get(id)
      .then((response) => {
        this.setState({
          currentMovie: response.data,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  updateStatus(status) {
    var data = {
      id: this.state.currentMovie.id,
      name: this.state.currentMovie.name,
      director: this.state.currentMovie.director,
      producer: this.state.currentMovie.producer,
      release: this.state.currentMovie.release,
      published: status,
    };

    this.props
      .updateMovie(this.state.currentMovie.id, data)
      .then((reponse) => {
        console.log(reponse);

        this.setState((prevState) => ({
          currentMovie: {
            ...prevState.currentMovie,
            published: status,
          },
        }));

        this.setState({ message: "The status was updated successfully!" });
      })
      .catch((e) => {
        console.log(e);
      });
  }

  updateContent() {
    this.props
      .updateMovie(this.state.currentMovie.id, this.state.currentMovie)
      .then((reponse) => {
        console.log(reponse);
        
        this.setState({ message: "The Movie was updated successfully!" });
      })
      .catch((e) => {
        console.log(e);
      });
  }

  removeMovie() {
    this.props
      .deleteMovie(this.state.currentMovie.id)
      .then(() => {
        this.props.history.push("/movies");
      })
      .catch((e) => {
        console.log(e);
      });
  }

  render() {
    const { currentMovie } = this.state;

    return (
      <div>
        {currentMovie ? (
          <div className="edit-form" >
            <h4>Movie</h4>
            <form >
              <div className="form-group">
                <label htmlFor="name" className="control-label">Name</label>
               
                <input
                  type="text"
                  className="form-control"
                  id="name"
                  value={currentMovie.name}
                  onChange={this.onChangeName}
                />
           
              </div>
              <div className="form-group">
                <label htmlFor="director">Director</label>
                <input
                  type="text"
                  className="form-control"
                  id="director"
                  value={currentMovie.director}
                  onChange={this.onChangeDirector}
                />
              </div>
              <div className="form-group">
                <label htmlFor="producer">Producer</label>
                <input
                  type="text"
                  className="form-control"
                  id="producer"
                  value={currentMovie.producer}
                  onChange={this.updateContent}
                />
              </div>
            
              <div className="form-group">
                <label htmlFor="release">Release</label>
                <input
                  type="text"
                  className="form-control"
                  id="release"
                  value={currentMovie.release}
                  onChange={this.updateContent}
                />
              </div>

              <div className="form-group">
                <label>
                  Movie Hit : 
                </label>
               {currentMovie.hit ? (
              <a
                className="link-success"
                onClick={() => this.updateStatus(false)}
              >
                True
              </a>
            ) : (
              <a
                className="link-danger"
                onClick={() => this.updateStatus(true)}
              >
                False
              </a>
            )}
              </div>
            </form>

            

            <button
              className="btn-danger mr-2"
              onClick={this.removeMovie}
            >
              Delete
            </button>

            <button
              type="submit"
              className="btn-success"
              onClick={this.updateContent}>
              Update
            </button>
            <p>{this.state.message}</p>
          </div>
        ) : (
          <div>
            <br />
            <p>Please click on a Movie...</p>
          </div>
        )}
      </div>
    );
  }
}

export default connect(null, { updateMovie, deleteMovie })(Movie);
