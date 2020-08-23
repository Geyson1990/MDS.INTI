import React from 'react';
import GridBase from "../../../Base/Tables/GridBase";
import { Card, CardBody, CardHeader } from 'reactstrap';

const GridPersonal = (props) => {
    const meta=[
        {
            key: 'N°',
            text: 'N°',
            sort: true,
          },
          {
            key: 'NumeroDocumento',
            text: 'NumeroDocumento',
            sort: true,
          }
    ];
    const datos=[];
    
    return (
        <Card>
            <CardHeader>
                <i className="icon-note"></i><strong>Resultados de búsqueda</strong>
            </CardHeader>
            <CardBody>
                <GridBase datos={datos} meta={meta}/>
            </CardBody>
        </Card>
    );
}

export default GridPersonal;