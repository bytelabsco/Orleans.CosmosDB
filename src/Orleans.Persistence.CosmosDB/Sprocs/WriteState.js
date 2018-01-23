function WriteState(entity) {
  var context = getContext();
  var collection = context.getCollection();
  var response = context.getResponse();

  var query = 'SELECT c.id, c._self FROM c WHERE c.grainType = "' + entity.grainType + '" AND c.id = "' + entity.id + '"';
  var accept = collection.queryDocuments(collection.getSelfLink(), query, {},
    function (err, docs, responseOptions) {
      if (err) throw new Error("Error: " + err.message);

      if (docs.length === 0) {
        var createAccepted = collection.createDocument(collection.getSelfLink(), entity,
          function (err, createdEntity) {
            if (err) throw new Error('Error creating StateEntity: ' + err.message);

            if (!createAccepted) throw new Error('Unable to create StateEntity');

            response.setBody(createdEntity._etag);
          });
      } else {
        var updateAccepted = collection.replaceDocument(docs[0]._self, entity,
          { accessCondition: { type: 'IfMatch', condition: entity._etag } },
          function (err, updatedEntity) {
            if (err) throw new Error('Error Updating StateEntity: ' + err.message);

            if (!updateAccepted) throw new Error('Unable to replace StateEntity');

            response.setBody(updatedEntity._etag);
          });
      }
    });
}
