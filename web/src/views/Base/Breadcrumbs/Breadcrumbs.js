import React, { useEffect, useState } from 'react';
import {  ListGroup, ListGroupItem, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
const items = [
  { number: "1", title: "🇦🇷 Argentina" },
  { number: "2", title: "🤩 YASS" },
  { number: "3", title: "👩🏼‍💻 Tech Girl" },
  { number: "4", title: "💋 Lipstick & Code" },
  { number: "5", title: "💃🏼 Latina" },
]

const initialDnDState = {
  draggedFrom: null,
  draggedTo: null,
  isDragging: false,
  originalOrder: [],
  updatedOrder: []
}

const Breadcrumbs = () => {
  const [list, setList] = useState(items);
  const [dragAndDrop, setDragAndDrop] = useState(initialDnDState);


  // onDragStart fires when an element
  // starts being dragged
  const onDragStart = (event) => {
    const initialPosition = Number(event.currentTarget.dataset.position);

    setDragAndDrop({
      ...dragAndDrop,
      draggedFrom: initialPosition,
      isDragging: true,
      originalOrder: list
    });


    // Note: this is only for Firefox.
    // Without it, the DnD won't work.
    // But we are not using it.
    event.dataTransfer.setData("text/html", '');
  }

  // onDragOver fires when an element being dragged
  // enters a droppable area.
  // In this case, any of the items on the list
  const onDragOver = (event) => {

    // in order for the onDrop
    // event to fire, we have
    // to cancel out this one
    event.preventDefault();

    let newList = dragAndDrop.originalOrder;

    // index of the item being dragged
    const draggedFrom = dragAndDrop.draggedFrom;

    // index of the droppable area being hovered
    const draggedTo = Number(event.currentTarget.dataset.position);

    const itemDragged = newList[draggedFrom];
    const remainingItems = newList.filter((item, index) => index !== draggedFrom);

    newList = [
      ...remainingItems.slice(0, draggedTo),
      itemDragged,
      ...remainingItems.slice(draggedTo)
    ];

    if (draggedTo !== dragAndDrop.draggedTo) {
      setDragAndDrop({
        ...dragAndDrop,
        updatedOrder: newList,
        draggedTo: draggedTo
      })
    }

  }

  const onDrop = (event) => {

    setList(dragAndDrop.updatedOrder);

    setDragAndDrop({
      ...dragAndDrop,
      draggedFrom: null,
      draggedTo: null,
      isDragging: false
    });
  }


  const onDragLeave = () => {
    setDragAndDrop({
      ...dragAndDrop,
      draggedTo: null
    });

  }

  // Not needed, just for logging purposes:
  useEffect(() => {
    console.log("Dragged From: ", dragAndDrop && dragAndDrop.draggedFrom);
    console.log("Dropping Into: ", dragAndDrop && dragAndDrop.draggedTo);
  }, [dragAndDrop])

  useEffect(() => {
    console.log("List updated!");
  }, [list])
  return (
    <div className="animated fadeIn">
      <Row>
        <Col xs="12">
          <Card>
            <CardHeader>
              <i className="fa fa-align-justify"></i><strong>Breadcrumbs</strong>
              <div className="card-header-actions">
                <a href="https://reactstrap.github.io/components/breadcrumbs/" rel="noreferrer noopener" target="_blank" className="card-header-action">
                  <small className="text-muted">docs</small>
                </a>
              </div>
            </CardHeader>
            <CardBody>
              <ListGroup>
                {list.map((item, index) => {
                  return (
                    <ListGroupItem tag="button" action
                      key={index}
                      data-position={index}
                      draggable

                      onDragStart={onDragStart}
                      onDragOver={onDragOver}
                      onDrop={onDrop}

                      onDragLeave={onDragLeave}

                      className={dragAndDrop && dragAndDrop.draggedTo === Number(index) ? "dropArea" : ""}
                    >
                      <span>{item.number}</span>
                      <p>{item.title}</p>
                      <i class="fas fa-arrows-alt-v"></i>
                      </ListGroupItem>
                  )
                })}

              </ListGroup>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </div>
  );

}

export default Breadcrumbs;
