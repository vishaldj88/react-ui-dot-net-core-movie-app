import React, { Component } from "react";
import { connect } from "react-redux";
import { createMovie } from "../actions/movies";

class AddMovie extends Component {
  constructor(props) {
    super(props);
    this.onChangeName = this.onChangeName.bind(this);
    this.onChangeDirector = this.onChangeDirector.bind(this);
    //this.emailId = this.onChangeEmail.bind(this);
    // this.gender = this.onChangeDescription.bind(this);
    // this.status = this.onChangeDescription.bind(this);
    this.saveMovie = this.saveMovie.bind(this);
    this.newMovie = this.newMovie.bind(this);

    this.state = {
      id: null,
      name: "",
      director: "",
      release: "",
      producer: "",
      hit: false,
      submitted: false,
    };
  }

  onChangeName(e) {
    this.setState({
      name: e.target.value,
    });
  }

  onChangeDirector(e) {
    this.setState({
      director: e.target.value,
    });
  }

  saveMovie() {
    const { name, director, release, producer, hit } = this.state;

    this.props
      .createMovie(name, director, producer, release, hit)
      .then((data) => {
        this.setState({
          id: data.id,
          name: data.name,
          director: data.director,
          hit: data.hit,
          release: data.release,
          producer: data.producer,
          submitted: true,
        });
        console.log(data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  newMovie() {
    this.setState({
      id: null,
      name: "",
      director: "",
      hit: false,
      release: "",
      producer: "",
      published: false,
      submitted: false,
    });
  }

  render() {
    return (
   
      <div className="submit-form">
        {this.state.submitted ? (
          <div>
            <h4>You submitted successfully!</h4>
            <button className="btn btn-success" onClick={this.newUser}>
              Add Movie
            </button>
          </div>
        ) : (
            <div>
              <div className="form-group">
                <label htmlFor="name">Name</label>
                <input
                  type="text"
                  className="form-control"
                  id="name"
                  required
                  value={this.state.name}
                  onChange={this.onChangeName}
                  name="name"
                />
              </div>

              <div className="form-group">
                <label htmlFor="director">Director</label>
                <input
                  type="text"
                  className="form-control"
                  id="director"
                  required
                  value={this.state.director}
                  onChange={this.onChangeDirector}
                  name="director"
                />
              </div>
              <div className="form-group">
                <label htmlFor="release">Release</label>
                <input
                  type="text"
                  className="form-control"
                  id="release"
                  required
                  value={this.state.release}
                  onChange={this.onChangeRelease}
                  name="release"
                />
              </div>

              <div className="form-group">
                <label htmlFor="producer">Producer</label>
                <input
                  type="text"
                  className="form-control"
                  id="producer"
                  required
                  value={this.state.producer}
                  onChange={this.onChangeProducer}
                  name="producer"
                />
              </div>

              <div className="form-group">
                <label htmlFor="hit">Hit</label>
                <input
                  type="text"
                  className="form-control"
                  id="hit"
                  required
                  value={this.state.hit}
                  onChange={this.onChangeHit}
                  name="hit"
                />
              </div>



              <button onClick={this.saveMovie} className="btn btn-success">
                Submit Movie
            </button>
            </div>
          )}
      </div>
    
    );
  }
}

export default connect(null, { createMovie })(AddMovie);
