<script>
	$(document).ready(function(){
		$('#btn-exportar').click(function(){
			window.location = "<?php echo $this->Html->Url(array('controller'=>'participantes','action'=>'exportar')); ?>";
		});
		
		$('#btn-filtrar').click(toggleFiltros);
		
		$('#ParticipanteFechaDesde').datepicker({dateFormat: "yyyy-mm-dd"});
		$('#ParticipanteFechaHasta').datepicker({dateFormat: "yyyy-mm-dd"});
	});
	
	function toggleFiltros(){
		if($('#filtros').is(':visible')){
			$('#filtros').hide();
		}else{
			$('#filtros').show();
		}
	}
</script>
<style>
.form-group{padding-right: 10px;}
#filtros{margin: 10px 0px; border: 1px solid #C0C0C0; padding: 10px;}
</style>
<div class="table-responsive mb-20">
	<button class="btn btn-primary btn-lg btn-expand" id="btn-exportar">Exportar Excel</button>
	<button class="btn btn-primary btn-lg btn-expand" id="btn-filtrar">Filtrar</button>
	<br>
	<div id="filtros" style="<?php if(isset($url_conditions)?'':'display: none;')?>">
		<?php echo $this->Form->create('Participante');
			echo '<table style="width: 100%;"><tr>';
				echo '<td><div class="form-group">'.$this->Form->input('Participante.evento_id',array('class'=>'form-control','empty'=>'Seleccione','value'=>(isset($url_conditions)?(isset($url_conditions['evento_id'])?$url_conditions['evento_id']:''):''))).'</div></td>';
				echo '<td><div class="form-group">'.$this->Form->input('Participante.fecha_desde',array('class'=>'form-control','type'=>'text','data-date-format'=>'yyyy-mm-dd','value'=>(isset($url_conditions)?(isset($url_conditions['fecha_desde'])?$url_conditions['fecha_desde']:''):''))).'</div></td>';
				echo '<td><div class="form-group">'.$this->Form->input('Participante.fecha_hasta',array('class'=>'form-control','type'=>'text','data-date-format'=>'yyyy-mm-dd','value'=>(isset($url_conditions)?(isset($url_conditions['fecha_hasta'])?$url_conditions['fecha_hasta']:''):''))).'</div></td>';
			echo '</tr></table>';
			?><button class="btn btn-primary">Filtrar</button><?php
		echo $this->Form->end(); ?>
	</div>
	<table class="table table-striped table-success" style="margin-top:10px;">
		<thead>
			<tr>
				<th>Nombre</th>
				<th>Apellido</th>
				<th>E-mail</th>
				<th>Teléfono</th>
				<th>DNI</th>
				<th>Provincia</th>
				<th>Evento</th>
				<th>Promotora</th>
				<th><?php echo $this->Paginator->sort('Participante.juego', 'Juego'); ?></th>
				<th><?php echo $this->Paginator->sort('Participante.fecha_ingresado', 'Fecha'); ?></th>
			</tr>
		</thead>
		<tbody>
			<?php foreach($data as $participante){ ?>
				<tr>
					<td><?php echo $participante['Participante']['nombre']; ?></td>
					<td><?php echo $participante['Participante']['apellido']; ?></td>
					<td><?php echo $participante['Participante']['email']; ?></td>
					<td><?php echo $participante['Participante']['telefono']; ?></td>
					<td><?php echo $participante['Participante']['dni']; ?></td>
					<td><?php echo $participante['Participante']['provincia']; ?></td>
					<td><?php echo $participante['Evento']['nombre']; ?></td>
					<td><?php echo $participante['Promotora']['nombre'].' '.$participante['Promotora']['apellido']; ?></td>
					<td><?php echo $participante['Participante']['juego_nombre']; ?></td>
					<td><?php echo date_format(date_create($participante['Participante']['fecha_ingresado']),'d-m-Y'); ?></td>
				</tr>
			<?php } ?>
		</tbody>
		<?php if(count($data) > 30){ ?>
		<tfoot>
			<tr>
				<th>Nombre</th>
				<th>Apellido</th>
				<th>E-mail</th>
				<th>Teléfono</th>
				<th>DNI</th>
				<th>Provincia</th>
				<th>Evento</th>
				<th>Promotora</th>
				<th>Juego</th>
				<th>Fecha</th>
			</tr>
		</tfoot>
		<?php } ?>
		<?php echo $this->Paginator->numbers(); ?>
	</table>
</div>