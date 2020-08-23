import React from 'react';
import FrmBusqueda from './components/FrmBusqueda';
import GridBase from '../../Base/Tables/GridBase'
import GridPersonal from './components/GridPersonal';

const BusquedaPersonal = () => {
    return (
        <div className="animated fadeIn">
            <FrmBusqueda />
            <GridPersonal />
        </div>
    );
}
export default BusquedaPersonal;