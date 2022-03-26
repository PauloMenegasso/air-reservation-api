import './App.css';

import {Home} from './routes//Home';
import {Airports} from './routes/Airports';
import {AirReservation} from './routes/AirReservation';
import { AddAirReservation } from './routes/AddAirReservation';
import { SearchAirReservation } from './routes/SearchAirReservations';
import { SearchAirReservationById } from './routes/SearchAirReservationById';
import { AddAirport } from './routes/AddAirport';
import {Navigation} from './Navigation';

import {BrowserRouter, Route, Routes} from 'react-router-dom';


function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <h3 className='m-3 d-flex justify-content-center'>
          Air Reservation App
        </h3>

        <Navigation/>

        <Routes>
          <Route path='/' element={<Home/>} exact> Home</Route> 
          <Route path='/airReservation' element={<AirReservation/>}> </Route>
          <Route path='/AddAirReservation' element={<AddAirReservation/>}> </Route>
          <Route path='/SearchAirReservation' element={<SearchAirReservation/>}> </Route>
          <Route path='/SearchAirReservationById' element={<SearchAirReservationById/>}> </Route>
          <Route path='/airports' element={<Airports/>}> </Route>
          <Route path='/addAirport' element={<AddAirport/>}> </Route>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
