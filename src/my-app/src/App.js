
import React from 'react';
import Contacts from './components/contacts';
import styles from './style.css';

  //fetch('http://jsonplaceholder.typicode.com/users')
  class App extends React.Component {
    
    state = {      
      contacts: [],      
      id:'',
      name:''
    };       
    
    componentDidMount() {
      // fetch('https://localhost:5001/api/categories')
      // //fetch('http://jsonplaceholder.typicode.com/users')
      // .then(res => res.json())
      // .then((data) => {
      //   this.setState({ contacts: data })
      // })
      // .catch(console.log)
    }

    handleGetClick = () => {
      fetch('https://localhost:5001/api/categories')
      .then(res => res.json())
      .then((data) => {
        this.setState({ contacts: data })
      })
      .catch(console.log)

      console.log('this is:', this);
    }

    handlePostClick = () => {
     
      console.log('Post Pressed:', this);
    }

    render() {
      return (

        <div className="container">
          <Contacts contacts={this.state.contacts} />   

          <button className={App.button} onClick={this.handleGetClick}>Get List</button>
          <button onClick={this.handlePostClick}>Add Item</button>    

          <form onSubmit={this.handleSubmit}>
            <label>
              Name:
              <input type="text" value={this.state.value} onChange={this.handleChange} />
            </label>
            <input type="submit" value="Submit" />
          </form>

      </div>
                 
              
      )
    }

  }

export default App;
