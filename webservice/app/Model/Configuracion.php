<?php
class Configuracion extends AppModel {
	var $name = 'Configuracion';
	var $useTable = 'configuraciones';
	
	/* Funciones */
	
	function getByNombreAndEvento($nombre, $evento_id){
		return $this->find('first',array('conditions'=>array('Configuracion.nombre'=>$nombre,'Configuracion.evento_id'=>$evento_id)));
	}
	
	function getByEvento($evento_id){
		return $this->find('list',array('conditions'=>array('Configuracion.evento_id'=>$evento_id),'fields'=>array('nombre','valor')));
	}
	
	function getAll(){
		$configuraciones = $this->find('all',array('fields'=>array('nombre','valor','evento_id')));
		$data = array();
		foreach($configuraciones as $configuracion){
			if(isset($data[$configuracion['Configuracion']['evento_id']])){
				$data[$configuracion['Configuracion']['evento_id']][$configuracion['Configuracion']['nombre']] = $configuracion['Configuracion']['valor'];
			}else{
				$data[$configuracion['Configuracion']['evento_id']] = array($configuracion['Configuracion']['nombre']=>$configuracion['Configuracion']['valor']);
			}
		}
		return $data;
	}
	
	function guardar($data){
		$response = array('status'=>false);
		$evento_id = $data['Configuracion']['evento_id'];
		foreach($data['Configuracion'] as $conf => $value){
			if($conf != 'evento_id' && $conf != ''){
				$save_data = array('evento_id'=>$evento_id,'nombre'=>$conf,'valor'=>$value);
				if($exists = $this->getByNombreAndEvento($conf,$evento_id)){
					$save_data['id'] = $exists['Configuracion']['id'];
				}else{
					$this->create();
				}
				if(!$this->save($save_data)){
					$response['status'] = false;
				}
			}
		}
		return $response;
	}
}
?>