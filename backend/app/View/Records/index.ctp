<div class="table-responsive mb-20">
	<table class="table table-striped table-success">
		<thead>
			<tr>
				<th>Descripción</th>
				<th>Valor</th>
				<th>Fecha</th>
			</tr>
		</thead>
		<tbody>
			<?php foreach($records as $record){ ?>
				<tr>
					<td><?php echo $record['Record']['name']; ?></td>
					<td><?php echo $record['Record']['value'].(strpos($record['Record']['name'],'device_id')!=false?' <a href="'.$this->Html->Url(array('controller'=>'tablets','action'=>'agregar',$record['Record']['value'])).'">Agregar Tablet</a>':''); ?></td>
					<td><?php echo $record['Record']['created']; ?></td>
				</tr>
			<?php } ?>
		</tbody>
	</table>
</div>