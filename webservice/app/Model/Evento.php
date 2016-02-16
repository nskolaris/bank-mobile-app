<?php
class Evento extends AppModel {
	var $name = 'Evento';

	public $validate = array(
        'code' => array(
			'on' => 'create',
            'rule' => array('checkCode'),
            'message' => 'Ya existe un evento con este código'
        ),
		'nombre' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar un nombre.'
		),
		'ciudad' => array(
			'rule' => 'notEmpty',
			'message' => 'Debe ingresar una ciudad.'
		)
    );
	
	function checkCode($data) {
        $codigo_usado = $this->find('count',array(
            'conditions' => $data,
            'recursive' => -1
        ));
        return $codigo_usado == 0;
    }
	
	/* Funciones */
	
	function getByCode($code){
		return $this->find('first',array('conditions'=>array('Evento.code'=>$code)));
	}
	
	function getById($id){
		return $this->find('first',array('conditions'=>array('Evento.id'=>$id)));
	}
	
	function getAll(){
		return $this->find('all',array('fields'=>array('id','code','nombre','ciudad','fecha_desde','fecha_hasta','deleted')));
	}
	
	function getCombo(){
		$eventos = $this->find('all',array('fields'=>array('id','code','nombre','deleted'),'order'=>'Evento.id ASC'));
		$combo = array();
		foreach($eventos as $evento){
			$combo[$evento['Evento']['id']] = $evento['Evento']['nombre'].' ('.$evento['Evento']['code'].')';
		}
		return $combo;
	}
	
	/*function beforeFind($query){
        parent::beforeFind();
        $defaultConditions = array('Evento.deleted' => null);
        $query['conditions'] = array_merge($query['conditions'], $defaultConditions);
        return $query;
    }*/
	
	function afterFind($results, $primary = false){
		foreach($results as $key => $val){
			if(isset($val['Evento'])){
				if(isset($val['Evento']['deleted']) && $val['Evento']['deleted'] != null){
					unset($results[$key]);
				}
				if(isset($val['deleted']) && $val['deleted'] != null){
					unset($results[$key]);
				}
			}
		}
		return $results;
	}
	
	function agregar($data){
		$response = array('status'=>false,'message'=>'No se pudo agregar el evento','errors'=>null);
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se agregó el evento con éxito';
		}
		return $response;
	}
	
	function modificar($data){
		$response = array('status'=>false,'message'=>'No se pudo modificar el evento');
		if($this->save($data)){
			$response['status'] = true;
			$response['message'] = 'Se modificó el evento con éxito';
		}
		return $response;
	}
	
	function borrar($id){
		$this->id = $id;
		return $this->saveField('deleted',date('Y-m-d h:i:s'));
	}
}
?>