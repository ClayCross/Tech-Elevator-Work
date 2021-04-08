import axios from 'axios';

const http = axios.create({
  baseURL: process.env.VUE_APP_REMOTE_API
})
export default{ 
  
  createBrewery(brewery, token){
    http.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return http.post('/breweries', brewery)
  },

  getUsers(username){
    let path = '/users';
    if(username){
      path += `?username=${username}`
    }
    return http.get(path);
  },

  getBreweries(){
      return http.get('/breweries');
  }
}